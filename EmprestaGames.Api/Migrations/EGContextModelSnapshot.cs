﻿// <auto-generated />
using System;
using EmprestaGames.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmprestaGames.Api.Migrations
{
    [DbContext(typeof(EGContext))]
    partial class EGContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EmprestaGames.Api.Models.Emprestimo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DataDevolvido");

                    b.Property<DateTime>("DataEmprestimo");

                    b.Property<int>("JogoId");

                    b.Property<int?>("PessoaDonoJogoId");

                    b.Property<int?>("PessoaEmprestadaId");

                    b.HasKey("Id");

                    b.HasIndex("JogoId");

                    b.HasIndex("PessoaDonoJogoId");

                    b.HasIndex("PessoaEmprestadaId");

                    b.ToTable("Emprestimos");
                });

            modelBuilder.Entity("EmprestaGames.Api.Models.Jogo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Jogos");
                });

            modelBuilder.Entity("EmprestaGames.Api.Models.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Pessoas");
                });

            modelBuilder.Entity("EmprestaGames.Api.Models.PessoaXJogo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("JogoId");

                    b.Property<int>("PessoaId");

                    b.HasKey("Id");

                    b.HasIndex("JogoId");

                    b.HasIndex("PessoaId");

                    b.ToTable("PessoasXJogos");
                });

            modelBuilder.Entity("EmprestaGames.Api.Models.Emprestimo", b =>
                {
                    b.HasOne("EmprestaGames.Api.Models.Jogo", "Jogo")
                        .WithMany()
                        .HasForeignKey("JogoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EmprestaGames.Api.Models.Pessoa", "PessoaDonoJogo")
                        .WithMany()
                        .HasForeignKey("PessoaDonoJogoId");

                    b.HasOne("EmprestaGames.Api.Models.Pessoa", "PessoaEmprestada")
                        .WithMany()
                        .HasForeignKey("PessoaEmprestadaId");
                });

            modelBuilder.Entity("EmprestaGames.Api.Models.PessoaXJogo", b =>
                {
                    b.HasOne("EmprestaGames.Api.Models.Jogo", "Jogos")
                        .WithMany()
                        .HasForeignKey("JogoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EmprestaGames.Api.Models.Pessoa", "Pessoas")
                        .WithMany()
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
