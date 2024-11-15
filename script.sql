IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [BranchSales] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_BranchSales] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CatServices] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_CatServices] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [NumbersDocuments] (
    [Id] int NOT NULL IDENTITY,
    [BranchId] int NOT NULL,
    [TypeDoc] nvarchar(max) NOT NULL,
    [SerieDoc] nvarchar(max) NOT NULL,
    [Number] int NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_NumbersDocuments] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [UserName] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Role] nvarchar(max) NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241003194628_InitialCreate', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [ProdServices] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [CatServiceId] int NOT NULL,
    CONSTRAINT [PK_ProdServices] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TypeDocuments] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_TypeDocuments] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241003195930_servicios_typeDoc', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [CatServices] ADD [Icon] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241003202848_catservice_icon_column', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProdServices]') AND [c].[name] = N'Status');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ProdServices] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [ProdServices] ALTER COLUMN [Status] nvarchar(1) NOT NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProdServices]') AND [c].[name] = N'Name');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ProdServices] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [ProdServices] ALTER COLUMN [Name] nvarchar(100) NOT NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProdServices]') AND [c].[name] = N'Description');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [ProdServices] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [ProdServices] ALTER COLUMN [Description] nvarchar(250) NOT NULL;
GO

CREATE TABLE [Customers] (
    [Id] int NOT NULL IDENTITY,
    [FirtsName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [Phone] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [DocPersonal] nvarchar(max) NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [WorkGuideMains] (
    [Id] int NOT NULL IDENTITY,
    [SerieGuia] nvarchar(max) NOT NULL,
    [NumeroGuia] nvarchar(max) NOT NULL,
    [FechaOperacion] datetime2 NOT NULL,
    [FechaHoraEntrega] datetime2 NOT NULL,
    [MensajeAlertas] nvarchar(max) NOT NULL,
    [Observaciones] nvarchar(max) NOT NULL,
    [TipoPago] int NOT NULL,
    [DescripcionPago] nvarchar(max) NOT NULL,
    [TipoRecepcion] int NOT NULL,
    [DireccionContacto] nvarchar(max) NOT NULL,
    [TelefonoContacto] nvarchar(max) NOT NULL,
    [Total] decimal(18,2) NOT NULL,
    [Acuenta] decimal(18,2) NOT NULL,
    [Saldo] decimal(18,2) NOT NULL,
    [CustomerId] int NOT NULL,
    CONSTRAINT [PK_WorkGuideMains] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WorkGuideMains_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [WorkGuideDetails] (
    [Id] int NOT NULL IDENTITY,
    [Cant] int NOT NULL,
    [Precio] decimal(18,2) NOT NULL,
    [Total] decimal(18,2) NOT NULL,
    [Observaciones] nvarchar(max) NOT NULL,
    [TipoLavado] nvarchar(max) NOT NULL,
    [Ubicacion] nvarchar(max) NOT NULL,
    [EstadoTrabajo] nvarchar(max) NOT NULL,
    [ProductId] int NOT NULL,
    [WorkGuideMainId] int NOT NULL,
    [EstadoRegistro] nvarchar(max) NOT NULL,
    [CompanyId] int NOT NULL,
    CONSTRAINT [PK_WorkGuideDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WorkGuideDetails_ProdServices_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [ProdServices] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_WorkGuideDetails_WorkGuideMains_WorkGuideMainId] FOREIGN KEY ([WorkGuideMainId]) REFERENCES [WorkGuideMains] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_WorkGuideDetails_ProductId] ON [WorkGuideDetails] ([ProductId]);
GO

CREATE INDEX [IX_WorkGuideDetails_WorkGuideMainId] ON [WorkGuideDetails] ([WorkGuideMainId]);
GO

CREATE INDEX [IX_WorkGuideMains_CustomerId] ON [WorkGuideMains] ([CustomerId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241008211129_customerok', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customers]') AND [c].[name] = N'Status');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Customers] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Customers] ALTER COLUMN [Status] nvarchar(1) NOT NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customers]') AND [c].[name] = N'Phone');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Customers] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Customers] ALTER COLUMN [Phone] nvarchar(20) NOT NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customers]') AND [c].[name] = N'LastName');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Customers] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Customers] ALTER COLUMN [LastName] nvarchar(100) NOT NULL;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customers]') AND [c].[name] = N'FirtsName');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Customers] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Customers] ALTER COLUMN [FirtsName] nvarchar(100) NOT NULL;
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customers]') AND [c].[name] = N'Email');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Customers] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Customers] ALTER COLUMN [Email] nvarchar(100) NOT NULL;
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customers]') AND [c].[name] = N'DocPersonal');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Customers] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Customers] ALTER COLUMN [DocPersonal] nvarchar(20) NOT NULL;
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customers]') AND [c].[name] = N'Address');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Customers] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Customers] ALTER COLUMN [Address] nvarchar(200) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241008212817_customerAjustes', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[WorkGuideMains]') AND [c].[name] = N'TipoRecepcion');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [WorkGuideMains] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [WorkGuideMains] ALTER COLUMN [TipoRecepcion] nvarchar(max) NOT NULL;
GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[WorkGuideMains]') AND [c].[name] = N'TipoPago');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [WorkGuideMains] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [WorkGuideMains] ALTER COLUMN [TipoPago] nvarchar(2) NOT NULL;
GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[WorkGuideDetails]') AND [c].[name] = N'Cant');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [WorkGuideDetails] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [WorkGuideDetails] ALTER COLUMN [Cant] decimal(18,2) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241010151715_guiaAjustes', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [WorkGuideDetails] ADD [EstadoPago] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [WorkGuideDetails] ADD [EstadoSituacion] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241014045657_ajustesguiadetalle', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [CashBoxMains] (
    [Id] int NOT NULL IDENTITY,
    [FechaCaja] datetime2 NOT NULL,
    [FechaHoraApertura] datetime2 NOT NULL,
    [FechaHoraCierre] datetime2 NOT NULL,
    [EstadoCaja] nvarchar(max) NOT NULL,
    [SaldoInicial] decimal(18,2) NOT NULL,
    [SaldoFinal] decimal(18,2) NOT NULL,
    [TotalIngreso] decimal(18,2) NOT NULL,
    [TotalSalida] decimal(18,2) NOT NULL,
    [Observaciones] nvarchar(max) NOT NULL,
    [ObservacionesCierre] nvarchar(max) NOT NULL,
    [EstadoRegistro] nvarchar(max) NOT NULL,
    [BranchSalesId] int NOT NULL,
    [WorkShiftId] int NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_CashBoxMains] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ExpenseBoxMains] (
    [Id] int NOT NULL IDENTITY,
    [CategoryGasto] nvarchar(max) NOT NULL,
    [PersonalAutoriza] nvarchar(max) NOT NULL,
    [FechaGasto] datetime2 NOT NULL,
    [Importe] decimal(18,2) NOT NULL,
    [DetallesEgreso] nvarchar(max) NOT NULL,
    [EstadoRegistro] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ExpenseBoxMains] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [WorkShifts] (
    [Id] int NOT NULL IDENTITY,
    [Descripcion] nvarchar(max) NOT NULL,
    [HoraInicio] time NOT NULL,
    [HoraCierre] time NOT NULL,
    [Observaciones] nvarchar(max) NOT NULL,
    [EstadoRegistro] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_WorkShifts] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CashBoxDetails] (
    [Id] int NOT NULL IDENTITY,
    [TipoComprobante] nvarchar(max) NOT NULL,
    [SerieComprobante] nvarchar(max) NOT NULL,
    [NumComprobante] nvarchar(max) NOT NULL,
    [FechaComprobante] datetime2 NOT NULL,
    [Importe] decimal(18,2) NOT NULL,
    [Adelanto] decimal(18,2) NOT NULL,
    [TipoPago] nvarchar(max) NOT NULL,
    [DescripcionPago] nvarchar(max) NOT NULL,
    [Observaciones] nvarchar(max) NOT NULL,
    [EstadoRegistro] nvarchar(max) NOT NULL,
    [CustomerId] int NOT NULL,
    [CashBoxMainId] int NOT NULL,
    CONSTRAINT [PK_CashBoxDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CashBoxDetails_CashBoxMains_CashBoxMainId] FOREIGN KEY ([CashBoxMainId]) REFERENCES [CashBoxMains] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CashBoxDetails_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_CashBoxDetails_CashBoxMainId] ON [CashBoxDetails] ([CashBoxMainId]);
GO

CREATE INDEX [IX_CashBoxDetails_CustomerId] ON [CashBoxDetails] ([CustomerId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241014154419_gastos_caja_horario', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241014234336_ajustescashbox', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [ExpenseBoxMains] ADD [CashBoxMainId] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [ExpenseBoxMains] ADD [UserId] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241016201157_adduser_Cashboxid_togastos', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CashBoxMains]') AND [c].[name] = N'FechaHoraCierre');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [CashBoxMains] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [CashBoxMains] ALTER COLUMN [FechaHoraCierre] datetime2 NULL;
GO

CREATE TABLE [BrachSalesUsers] (
    [Id] int NOT NULL IDENTITY,
    [BranchSalesId] int NOT NULL,
    [UserId] int NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_BrachSalesUsers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BrachSalesUsers_BranchSales_BranchSalesId] FOREIGN KEY ([BranchSalesId]) REFERENCES [BranchSales] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BrachSalesUsers_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_BrachSalesUsers_BranchSalesId] ON [BrachSalesUsers] ([BranchSalesId]);
GO

CREATE INDEX [IX_BrachSalesUsers_UserId] ON [BrachSalesUsers] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241018150917_addsucursalesUsuario', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[NumbersDocuments].[Number]', N'NumberDoc', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241026192657_changeColumnNumberDoc', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [WorkGuideMains] ADD [EstadoPago] nvarchar(2) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241102051135_addEstadoPago_col_guiacab', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [WorkGuideMains] ADD [EstadoRegistro] nvarchar(2) NOT NULL DEFAULT N'';
GO

ALTER TABLE [WorkGuideMains] ADD [EstadoSituacion] nvarchar(2) NOT NULL DEFAULT N'';
GO

ALTER TABLE [WorkGuideMains] ADD [FechaPago] datetime2 NULL;
GO

ALTER TABLE [WorkGuideMains] ADD [FechaRecojo] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241102161417_addcolspagorecojoestado', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [WorkGuideMains] ADD [AlertMsgId] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [WorkGuideMains] ADD [AlertMsgId1] int NULL;
GO

CREATE TABLE [AlertMsgs] (
    [Id] int NOT NULL IDENTITY,
    [Titulo] nvarchar(max) NOT NULL,
    [TipoAlerta] nvarchar(max) NOT NULL,
    [Mensaje] nvarchar(max) NOT NULL,
    [WorkGuideMainId] int NOT NULL,
    CONSTRAINT [PK_AlertMsgs] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_WorkGuideMains_AlertMsgId1] ON [WorkGuideMains] ([AlertMsgId1]);
GO

ALTER TABLE [WorkGuideMains] ADD CONSTRAINT [FK_WorkGuideMains_AlertMsgs_AlertMsgId1] FOREIGN KEY ([AlertMsgId1]) REFERENCES [AlertMsgs] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241104000426_addalertas', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [WorkGuideMains] ADD [TipoPagoCancelacion] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241104042307_addcoltipopagocancelacion', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [WorkGuideDetails] ADD [FechaRecojo] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241106160904_addcolfecharcojo', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [WorkGuideDetails] ADD [FechaDevolucion] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241106165246_addcolfechadevolucion', N'8.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [CashBoxDetails] DROP CONSTRAINT [FK_CashBoxDetails_Customers_CustomerId];
GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CashBoxDetails]') AND [c].[name] = N'CustomerId');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [CashBoxDetails] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [CashBoxDetails] ALTER COLUMN [CustomerId] int NULL;
GO

ALTER TABLE [CashBoxDetails] ADD CONSTRAINT [FK_CashBoxDetails_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241111143729_addcolnullclientecajadetalle', N'8.0.8');
GO

COMMIT;
GO

