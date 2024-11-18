﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace _24dex_backend_comercial.Migrations
{
    [DbContext(typeof(RecepcionDbContext))]
    [Migration("20241115144619_addcolsIsPesoIsLavado")]
    partial class addcolsIsPesoIsLavado
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AlertMsg", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Mensaje")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoAlerta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkGuideMainId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AlertMsgs");
                });

            modelBuilder.Entity("BrachSalesUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BranchSalesId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BranchSalesId");

                    b.HasIndex("UserId");

                    b.ToTable("BrachSalesUsers");
                });

            modelBuilder.Entity("BranchSales", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BranchSales");
                });

            modelBuilder.Entity("CashBoxDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Adelanto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CashBoxMainId")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("DescripcionPago")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstadoRegistro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaComprobante")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Importe")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("NumComprobante")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observaciones")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SerieComprobante")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoComprobante")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoPago")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CashBoxMainId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CashBoxDetails");
                });

            modelBuilder.Entity("CashBoxMain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BranchSalesId")
                        .HasColumnType("int");

                    b.Property<string>("EstadoCaja")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstadoRegistro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCaja")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaHoraApertura")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaHoraCierre")
                        .HasColumnType("datetime2");

                    b.Property<string>("Observaciones")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ObservacionesCierre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SaldoFinal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SaldoInicial")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalIngreso")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalSalida")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WorkShiftId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CashBoxMains");
                });

            modelBuilder.Entity("Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DocPersonal")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirtsName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("DexterCompany.Models.CatService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CatServices");
                });

            modelBuilder.Entity("DexterCompany.Models.ProdService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CatServiceId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("IsLavado")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("IsPeso")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.HasKey("Id");

                    b.ToTable("ProdServices");
                });

            modelBuilder.Entity("DexterCompany.Models.TypeDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TypeDocuments");
                });

            modelBuilder.Entity("ExpenseBox", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CashBoxMainId")
                        .HasColumnType("int");

                    b.Property<string>("CategoryGasto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetallesEgreso")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstadoRegistro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaGasto")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Importe")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PersonalAutoriza")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ExpenseBoxMains");
                });

            modelBuilder.Entity("NumbersDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<int>("NumberDoc")
                        .HasColumnType("int");

                    b.Property<string>("SerieDoc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeDoc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("NumbersDocuments");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WorkGuideDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Cant")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("EstadoPago")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstadoRegistro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstadoSituacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstadoTrabajo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaDevolucion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaRecojo")
                        .HasColumnType("datetime2");

                    b.Property<string>("Observaciones")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("TipoLavado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Ubicacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkGuideMainId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("WorkGuideMainId");

                    b.ToTable("WorkGuideDetails");
                });

            modelBuilder.Entity("WorkGuideMain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Acuenta")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("AlertMsgId")
                        .HasColumnType("int");

                    b.Property<int?>("AlertMsgId1")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("DescripcionPago")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DireccionContacto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstadoPago")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("EstadoRegistro")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("EstadoSituacion")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<DateTime>("FechaHoraEntrega")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaOperacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaPago")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaRecojo")
                        .HasColumnType("datetime2");

                    b.Property<string>("MensajeAlertas")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroGuia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observaciones")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Saldo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SerieGuia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelefonoContacto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoPago")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("TipoPagoCancelacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoRecepcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("AlertMsgId1");

                    b.HasIndex("CustomerId");

                    b.ToTable("WorkGuideMains");
                });

            modelBuilder.Entity("WorkShift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstadoRegistro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("HoraCierre")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("HoraInicio")
                        .HasColumnType("time");

                    b.Property<string>("Observaciones")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WorkShifts");
                });

            modelBuilder.Entity("BrachSalesUser", b =>
                {
                    b.HasOne("BranchSales", "BranchSales")
                        .WithMany("BrachSalesUsers")
                        .HasForeignKey("BranchSalesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", "User")
                        .WithMany("BrachSalesUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BranchSales");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CashBoxDetail", b =>
                {
                    b.HasOne("CashBoxMain", "CashBoxMain")
                        .WithMany("CashBoxDetails")
                        .HasForeignKey("CashBoxMainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.Navigation("CashBoxMain");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("WorkGuideDetail", b =>
                {
                    b.HasOne("DexterCompany.Models.ProdService", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkGuideMain", "WorkGuideMain")
                        .WithMany("WorkGuideDetails")
                        .HasForeignKey("WorkGuideMainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("WorkGuideMain");
                });

            modelBuilder.Entity("WorkGuideMain", b =>
                {
                    b.HasOne("AlertMsg", "AlertMsg")
                        .WithMany()
                        .HasForeignKey("AlertMsgId1");

                    b.HasOne("Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AlertMsg");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BranchSales", b =>
                {
                    b.Navigation("BrachSalesUsers");
                });

            modelBuilder.Entity("CashBoxMain", b =>
                {
                    b.Navigation("CashBoxDetails");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Navigation("BrachSalesUsers");
                });

            modelBuilder.Entity("WorkGuideMain", b =>
                {
                    b.Navigation("WorkGuideDetails");
                });
#pragma warning restore 612, 618
        }
    }
}