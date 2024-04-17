using AIDotNet.Document.Contract.Models;
using AIDotNet.Document.Services.Domain;

namespace AIDotNet.Document.Services.Services.Test
{
    /// <summary>
    /// 测试用
    /// </summary>
    /// <param name="db"></param>
    public class MemoryFolderService(FolderDb db) : IFolderService
    {
        readonly List<Folder> fakeDb = db.List;
        public async Task<List<FolderItemDto>> GetTreeFolderAsync()
        {
            var rootFolders = await fakeDb.Where(f => f.ParentId == null && f.IsFolder).ToListAsync();

            return rootFolders
                .Select(x => new FolderItemDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    ParentId = x.ParentId
                })
                .ToList();
        }

        public async Task CreateAsync(FolderItemDto folder)
        {
            if (folder.IsFolder)
            {
                await fakeDb.AddAsync(new Folder(folder.Name, folder.ParentId));
            }
            else
            {
                await fakeDb.AddAsync(new Folder(folder.Name, folder.ParentId, folder.Size));
            }
        }

        public async Task RemoveAsync(string id)
        {
            await fakeDb.DeleteAsync(id);
        }

        public async Task UpdateAsync(FolderItemDto folder)
        {
            await fakeDb.UpdateAsync(new Folder(folder.Name, folder.ParentId)
            {
                Id = folder.Id,
                Size = folder.Size,
            });
        }

        public async Task<List<FolderItemDto>> GetFolderByParentIdAsync(string? parentId)
        {
            var folders = await fakeDb.GetFolderByParentIdAsync(parentId);

            return folders
                .Select(x => new FolderItemDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    ParentId = x.ParentId
                })
                .ToList();
        }
    }
}
