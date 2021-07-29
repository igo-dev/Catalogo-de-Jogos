using Catalogo_de_jogos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo_de_jogos.Interfaces
{
    public interface IJogoRepository : IDisposable
    {
        Task<List<JogoModel>> Obter(int pag, int qnt);
        Task<JogoModel> Obter(Guid id);
        Task<List<JogoModel>> Obter(string nome, string produtora);
        Task Inserir(JogoModel jogoModel);
        Task Atualizar(JogoModel jogoModel);
        Task Remover(Guid id);
    }
}
