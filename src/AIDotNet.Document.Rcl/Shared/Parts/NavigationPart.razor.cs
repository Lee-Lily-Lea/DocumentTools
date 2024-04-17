namespace AIDotNet.Document.Rcl.Shared.Parts
{
    public partial class NavigationPart
    {
        [Parameter]
        public StringNumber Width { get; set; }

        private List<FolderItemDto> folderItems = new();

        private StringNumber _selectedItem;

        private bool _myFolderExpanded = false;

        void OnMyFolderExpandedChanged(bool value)
        {
            if ((value, _myFolderExpanded) is (true, false))
            {
                NavigationManager.NavigateTo("/my-folder");
            }

            _myFolderExpanded = value;
        }

        private string tableContextMenu = Guid.NewGuid().ToString("N");

        private async Task OnBlurAsync(FolderItemDto itemDto)
        {
            itemDto.IsEdit = false;
            await FolderService.UpdateAsync(itemDto);
            await InvokeAsync(StateHasChanged);
        }

        private async Task NewFolder(string? parentId)
        {
            _myFolderExpanded = true;

            if (parentId == null)
            {
                var folder = new FolderItemDto
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "新建文件夹",
                    IsEdit = true,
                    ParentId = parentId
                };

                folderItems.Add(folder);
                await FolderService.CreateAsync(folder);
            }
            else
            {
                await FolderService.CreateAsync(
                    new FolderItemDto()
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        Name = "新建文件夹",
                        IsEdit = true,
                        ParentId = parentId
                    }
                );
            }
        }

        private async ValueTask LoadAsync()
        {
            folderItems = await FolderService.GetTreeFolderAsync();

            await InvokeAsync(StateHasChanged);
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task RemoveAsync(string id)
        {
            folderItems.Remove(folderItems.First(x => x.Id == id));

            await FolderService.RemoveAsync(id);

            await LoadAsync();
        }

        private void UpdateName(FolderItemDto item)
        {
            item.IsEdit = true;

            InvokeAsync(StateHasChanged);
        }

        private void SelectItem(FolderItemDto item)
        {
            _selectedItem = item.Id;

            NavigationManager.NavigateTo($"/my-folder?folderId={item.Id}");
        }
    }
}
