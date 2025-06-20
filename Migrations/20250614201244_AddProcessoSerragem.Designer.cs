﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaMamoreGranito.Data;

#nullable disable

namespace SistemaMamoreGranito.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250614201244_AddProcessoSerragem")]
    partial class AddProcessoSerragem
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SistemaMamoreGranito.Models.Bloco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Altura")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Comprimento")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("DataEntrada")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Disponivel")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("Largura")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("NomeMaterial")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("NumeroNotaFiscal")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Observacoes")
                        .HasColumnType("longtext");

                    b.Property<string>("PedreiraOrigem")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Peso")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("TipoMaterial")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("ValorCompra")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("Blocos");
                });

            modelBuilder.Entity("SistemaMamoreGranito.Models.Chapa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Altura")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("BlocoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataEntrada")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Disponivel")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("Espessura")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Largura")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("NomeMaterial")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Observacoes")
                        .HasColumnType("longtext");

                    b.Property<decimal>("Peso")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("TipoMaterial")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("BlocoId");

                    b.ToTable("Chapas");
                });

            modelBuilder.Entity("SistemaMamoreGranito.Models.ProcessoSerragem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BlocoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataProcesso")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("EspessuraChapa")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Observacoes")
                        .HasColumnType("longtext");

                    b.Property<int>("QuantidadeChapas")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BlocoId");

                    b.ToTable("ProcessosSerragem");
                });

            modelBuilder.Entity("SistemaMamoreGranito.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Ativo")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("TipoUsuario")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("SistemaMamoreGranito.Models.Chapa", b =>
                {
                    b.HasOne("SistemaMamoreGranito.Models.Bloco", "BlocoOrigem")
                        .WithMany()
                        .HasForeignKey("BlocoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("BlocoOrigem");
                });

            modelBuilder.Entity("SistemaMamoreGranito.Models.ProcessoSerragem", b =>
                {
                    b.HasOne("SistemaMamoreGranito.Models.Bloco", "Bloco")
                        .WithMany()
                        .HasForeignKey("BlocoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Bloco");
                });
#pragma warning restore 612, 618
        }
    }
}
