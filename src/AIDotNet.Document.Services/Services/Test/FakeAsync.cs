using AIDotNet.Document.Services.Domain;

namespace AIDotNet.Document.Services.Services.Test
{
    internal static class FakeAsync
    {
        const int FakeDelay = 300;

        public static async Task<List<Folder>> ToListAsync(this IEnumerable<Folder> iq)
        {
            await Task.Delay(FakeDelay);
            return iq.ToList();
        }

        public static async Task<bool> AddAsync(this List<Folder> db, Folder model)
        {
            ArgumentNullException.ThrowIfNull(model);

            await Task.Delay(FakeDelay);

            db.Add(model);

            return true;
        }

        public static async Task<bool> DeleteAsync(this List<Folder> db, string id)
        {
            ArgumentNullException.ThrowIfNull(id);

            await Task.Delay(FakeDelay);

            var models = db.FindAll(x => x.Id == id);
            if (models.Count != 1)
            {
                return false;
            }

            return db.Remove(models[0]);
        }


        public static async Task<bool> UpdateAsync(this List<Folder> db, Folder model)
        {
            ArgumentNullException.ThrowIfNull(model);

            await Task.Delay(FakeDelay);

            var models = db.FindAll(x => x.Id == model.Id);
            if (models.Count != 1)
            {
                return false;
            }

            models[0].Name = model.Name;
            models[0].ParentId = model.ParentId;
            models[0].Size = model.Size;

            return true;
        }

        public static async Task<List<Folder>> GetFolderByParentIdAsync(this List<Folder> db, string? parentId)
        {
            await Task.Delay(FakeDelay);

            var models = db.FindAll(x => x.ParentId == parentId);

            return models;
        }
    }
}
