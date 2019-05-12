using mc.core.domain.Interface.Repository;
using mc.core.domain.register.Entity;
using mc.core.domain.register.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace mc.crawler.cep
{
    public class CepPieces
    {
        private ICityRepository _city;
        private IStateRepository _state;
        private ICountryRepository _country;

        public CepPieces(ICityRepository city, IStateRepository state, ICountryRepository country)
        {
            this._city = city;
            this._state = state;
            this._country = country;
        }

        private T GetData<T>(Expression<Func<T, bool>> func, IBaseRepository<T> repository) where T: class
        {
            var result = repository.GetData(func);

            if (!result.Any())
            {
                return null;
            }

            return result.FirstOrDefault();
        }

        public City GetCity(Expression<Func<City, bool>> func)
        {
            return this.GetData(func, this._city);
        }

        public State GetState(Expression<Func<State, bool>> func)
        {
            return this.GetData(func, this._state);
        }

        public Country GetCoutry(Expression<Func<Country, bool>> func)
        {
            return this.GetData(func, this._country);
        }
    }
}
