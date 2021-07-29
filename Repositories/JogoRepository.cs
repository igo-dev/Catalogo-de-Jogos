using Catalogo_de_jogos.Interfaces;
using Catalogo_de_jogos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo_de_jogos.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private readonly JogoContext _jogoContext;

        public JogoRepository(JogoContext jogoContext)
        {
            _jogoContext = jogoContext;
        }

        public Task Atualizar(JogoModel jogoModel)
        {
            _jogoContext.Jogos.Update(jogoModel);
            _jogoContext.SaveChanges();
            return Task.FromResult(_jogoContext.SaveChangesAsync());
        }

        public async void Dispose()
        {
            await _jogoContext.DisposeAsync();
        }

        public async Task Inserir(JogoModel jogoModel)
        {
            _jogoContext.Jogos.Add(jogoModel);
            await _jogoContext.SaveChangesAsync();
        }

        public async Task<List<JogoModel>> Obter(int pag, int qnt)
        {
            return Task.FromResult(_jogoContext.Jogos.Skip((pag - 1) * qnt).Take(qnt).ToList()).Result;
        }

        public async Task<JogoModel> Obter(Guid id)
        {
            return Task.FromResult(_jogoContext.Jogos.Find(id)).Result;
        }

        public async Task<List<JogoModel>> Obter(string nome, string produtora)
        {
            return Task.FromResult(_jogoContext.Jogos.Where(jogo => jogo.Nome == nome && jogo.Produtora == produtora).ToList()).Result;
        }

        public async Task Remover(Guid id)
        {
            var jogo =  await _jogoContext.Jogos.FindAsync(id);
            _jogoContext.Jogos.Remove(jogo);
            await _jogoContext.SaveChangesAsync();
        }
    }
}
