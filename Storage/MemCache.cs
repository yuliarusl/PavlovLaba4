using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Storage
{
    public class MemCache : IStorage<PhonesData>
    {
        private object _sync = new object();
        private List<PhonesData> _memCache = new List<PhonesData>();
        public PhonesData this[Guid id]
        {
            get
            {
                lock (_sync)
                {
                    if (!Has(id)) throw new IncorrectPhonesDataException($"No PhonesData with id {id}");

                    return _memCache.Single(x => x.Id == id);
                }
            }
            set
            {
                if (id == Guid.Empty) throw new IncorrectPhonesDataException("Cannot request PhonesData with an empty id");

                lock (_sync)
                {
                    if (Has(id))
                    {
                        RemoveAt(id);
                    }

                    value.Id = id;
                    _memCache.Add(value);
                }
            }
        }

        public System.Collections.Generic.List<PhonesData> All => _memCache.Select(x => x).ToList();

        public void Add(PhonesData value)
        {
            if (value.Id != Guid.Empty) throw new IncorrectPhonesDataException($"Cannot add value with predefined id {value.Id}");

            value.Id = Guid.NewGuid();
            this[value.Id] = value;
        }

        public bool Has(Guid id)
        {
            return _memCache.Any(x => x.Id == id);
        }

        public void RemoveAt(Guid id)
        {
            lock (_sync)
            {
                _memCache.RemoveAll(x => x.Id == id);
            }
        }
    }
}
