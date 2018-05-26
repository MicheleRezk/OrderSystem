using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Application.Interfaces
{
    public interface IJsonService
    {
        T ReadJsonFile<T>(string filePath) where T : class;
        void WriteJsonFile<T>(T data,string filePath) where T : class;
    }
}
