using Catalogo_de_jogos.Dtos;
using Catalogo_de_jogos.Exceptions;
using Catalogo_de_jogos.Interfaces;
using Catalogo_de_jogos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo_de_jogos.Services
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task Atualizar(Guid id, JogoDto jogo)
        {
            var hasJogo = await _jogoRepository.Obter(id);
            if (hasJogo == null)
                throw new JogoNaoCadastradoException();

            hasJogo.Nome = jogo.Nome;
            hasJogo.Produtora = jogo.Produtora;
            hasJogo.Preco = jogo.Preco;

            await _jogoRepository.Atualizar(hasJogo);
        }

        public async Task AtualizarPreco(Guid id, double preco)
        {
            var hasJogo = await _jogoRepository.Obter(id);
            if (hasJogo == null)
                throw new JogoNaoCadastradoException();

            hasJogo.Preco = preco;
            await _jogoRepository.Atualizar(hasJogo);
        }

        public async Task<JogoModel> Inserir(JogoDto jogo)
        {
            var hasJogo = await _jogoRepository.Obter(jogo.Nome, jogo.Produtora);

            if (hasJogo.Count != 0)
                throw new JogoJaCadastradoException();

            var jogoInsert = new JogoModel
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Preco = jogo.Preco,
                Produtora = jogo.Produtora
            };

            await _jogoRepository.Inserir(jogoInsert);

            return jogoInsert;
        }

        public async Task<List<JogoModel>> Obter(int pag, int qnt)
        {
            var jogos = await _jogoRepository.Obter(pag, qnt);

            return  jogos.Select(j => new JogoModel
            {
                Id = j.Id,
                Nome = j.Nome,
                Produtora = j.Produtora,
                Preco = j.Preco

            }).ToList();
        }

        public async Task<JogoModel> Obter(Guid id)
        {
            var jogo = await _jogoRepository.Obter(id);
            if (jogo == null)
                return null;

            return new JogoModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task Remover(Guid id)
        {
            var hasJogo = await _jogoRepository.Obter(id);

            if (hasJogo == null)
                throw new JogoJaCadastradoException();

            await _jogoRepository.Remover(id);
        }

        public void Dispose()
        {
            _jogoRepository?.Dispose();
        }
    }
}
