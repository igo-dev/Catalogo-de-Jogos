using Catalogo_de_jogos.Dtos;
using Catalogo_de_jogos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo_de_jogos.Interfaces
{
    public interface IJogoService : IDisposable
    {
        Task<List<JogoModel>> Obter(int pag, int qnt);
        Task<JogoModel> Obter(Guid id);
        Task<JogoModel> Inserir(JogoDto jogo);
        Task Atualizar(Guid id, JogoDto jogo);
        Task AtualizarPreco(Guid id, double preco);
        Task Remover(Guid id);
    }
}
