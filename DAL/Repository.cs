using DAL.Contracts;
using DAL.Exceptions;
using Domains;
using Utilites;

namespace DAL
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {

        IConverter<T> _converter;


        public Repository(IConverter<T> converter)
        {
            _converter = converter;
            FileName = GetFileName();
        }



        string FileName = "";


        public async Task<List<T>> GetAll()
        {
            try
            {
                var items = _converter.ConvertBack(await FileHelper.ReadAll(FileName));
                return items;
            }
            catch (SerlizationnExecption ex)
            {
                return new List<T>();
            }
            catch (Exception ex)
            {

                throw new DataAcessException("Error reading items from data source.","list is now null",ex);
            }
        }



        public async Task<T> GetById(int id)
        {
            try
            {
                var items = _converter.ConvertBack(await FileHelper.ReadAll(FileName));
                var item = items.Where(a => a.Id == id).FirstOrDefault();
                if (item == null)
                    return new T();
                return item;
            }
            catch (SerlizationnExecption eex)
            {
                return new T();
            }
            catch (Exception ex)
            {
                throw new DataAcessException($"Error reading item with id {id} from data source.", "item is now null", ex);
            }
        }




        public async Task Add(T model)
        {
            try
            {
                var items = await GetAll();
                items.Add(model);

                var itemString = _converter.ConvertData(items);
                await FileHelper.Create(FileName, itemString);
            }
            catch (Exception ex)
            {
                throw new DataAcessException(
                    "Error adding item to data source.",
                    "item not added",
                    ex);
            }
        }



        public async Task Update(T model)
        {
            try
            {
                var items = await GetAll();

                var existingItem = items.Where(a => a.Id == model.Id).FirstOrDefault();

                if (existingItem != null)
                {
                    items.Remove(existingItem);
                }

                items.Add(model);

                var itemString = _converter.ConvertData(items);

                await FileHelper.Create(FileName, itemString);
            }
            catch (Exception ex)
            {
                throw new DataAcessException(
                    $"Error updating item with id {model.Id} in data source.",
                    "item not updated",
                    ex);
            }
        }




        public async Task Delete(int id)
        {
            try
            {
                // get all
                var items = await GetAll();
                // get item by id
                var itemToRemove = items.FirstOrDefault(i => i.Id == id);
                // remove item
                if (itemToRemove != null)
                {
                    items.Remove(itemToRemove);
                }
                // save all back
                var itemString = _converter.ConvertData(items);
                await FileHelper.Update(FileName, itemString);
            }
            catch (Exception ex)
            {
                throw new DataAcessException($"Error deleting item with id {id} from data source.", "item not deleted", ex);
            }
        }



        string GetFileName()
        {
            var typeName = typeof(T).Name.ToLower();
            return $"{typeName}s.json";
        }


    }
}
