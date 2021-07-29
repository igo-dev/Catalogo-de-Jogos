﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo_de_jogos.Dtos
{
    public class JogoDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogo deve conter entre 3 e 100 caracteres.")]
        public string Nome { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da produtora deve conter entre 3 e 100 caracteres.")]
        public string Produtora { get; set; }
        [Required]
        [Range(0, 1000, ErrorMessage = "O preco do jogo deve ser de no minimo 1 real e no maximo 1000 reais.")]
        public double Preco { get; set; }
    }
}