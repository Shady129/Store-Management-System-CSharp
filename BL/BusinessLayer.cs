using BL.Contracts;
using BL.Execptions;
using DAL;
using DAL.Contracts;
using DAL.Exceptions;
using DAL.FileConverters;
using System.ComponentModel.DataAnnotations;


namespace BL
{
    public class BusinessLayer<T> where T : Domains.BaseEntity,new()
    {

        public BusinessLayer(StorageType type)
        {
            switch (type)
            {
                case StorageType.JsonFile:
                    _repository = new Repository<T>(new JsonConverter<T>());
                    break;

                case StorageType.TextFile:
                    _repository = new Repository<T>(new TextFileFormatConverter<T>());
                    break;
            }
        }

        IRepository<T> _repository;



        public async Task Add(T model)
        {

            try
            {
                if (!Validate(model))
                    throw new ValidationExecption("", "");


                var items = await _repository.GetAll();

                if (items.Any(x => x.Id == model.Id))
                
                    throw new ValidationExecption("", "");
                

                    await _repository.Add(model);
            }
            catch(DataAcessException ex)
            {

                throw new BusinessExecption("", "", ex);
            }

        }




        public async Task<T> GetById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ValidationExecption("", "");

                return await _repository.GetById(id);
            }
            catch (DataAcessException ex)
            {
                throw new BusinessExecption("", "", ex);
            }

        }



        public async Task Delete(int id)
        {
            var item = await _repository.GetById(id);

            if (item == null || item.Id == 0)
                throw new ValidationExecption("", "");

            try
            {
                await _repository.Delete(id);
            }
            catch (DataAcessException ex)
            {
                throw new BusinessExecption("", "");
            }
        }





        public async Task<List<T>> GetAll()
        {
            try
            {
                var items = await _repository.GetAll();

                if (items == null)
                    throw new NotFoundExecption("", "");

                return items;
            }
            catch (DataAcessException ex)
            {
                throw new BusinessExecption("", "", ex);
            }
        }






        public async Task Update(T model)
        {
            try
            {
                if (!Validate(model))
                    throw new ValidationExecption("", "");

                await _repository.Update(model);
            }
            catch (DataAcessException ex)
            {
                throw new BusinessExecption("", "", ex);
            }
        }





        public bool Validate(T model)
        {
            IValidator<T> validator = new Validator<T>();
            return validator.Validate(model);
        }


    }
}
