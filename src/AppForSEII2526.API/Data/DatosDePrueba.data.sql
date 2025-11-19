SET IDENTITY_INSERT [dbo].[Maintenances] ON
INSERT INTO [dbo].[Maintenances] ([Id], [Name], [NumberOfDays], [Price]) VALUES (1, N'Hola', 8, CAST(70.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Maintenances] OFF

SET IDENTITY_INSERT [dbo].[MaintenanceTypes] ON
INSERT INTO [dbo].[MaintenanceTypes] ([id], [Type], [MaintenanceId]) VALUES (6, N'holaa', 1)
SET IDENTITY_INSERT [dbo].[MaintenanceTypes] OFF

SET IDENTITY_INSERT [dbo].[Models] ON
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (1, N'FIAT')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (2, N'KIA')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (3, N'SEAT')
SET IDENTITY_INSERT [dbo].[Models] OFF

SET IDENTITY_INSERT [dbo].[Cars] ON
INSERT INTO [dbo].[Cars] ([Id], [ModelId], [CarClass], [Color], [Description], [EngDispacement], [FuelType], [MaintenanceTypesid], [Manufacturer], [PurchasingPrice], [QuantityForPurchasing], [QuantityForRenting], [RentingPrice], [RimSize]) VALUES (13, 1, N'Fiat', N'Rojo', N'TontoElQueLoLea', N'NoSeQueEs', N'Gasolina', 6, N'HOLA', CAST(50.00 AS Decimal(10, 2)), 30, 8, 30, 5)
INSERT INTO [dbo].[Cars] ([Id], [ModelId], [CarClass], [Color], [Description], [EngDispacement], [FuelType], [MaintenanceTypesid], [Manufacturer], [PurchasingPrice], [QuantityForPurchasing], [QuantityForRenting], [RentingPrice], [RimSize]) VALUES (17, 2, N'KIA', N'Negro', N'Hola', N'nose', N'Gasoil', 6, N'No', CAST(40.00 AS Decimal(10, 2)), 300, 200, 200, 20)
INSERT INTO [dbo].[Cars] ([Id], [ModelId], [CarClass], [Color], [Description], [EngDispacement], [FuelType], [MaintenanceTypesid], [Manufacturer], [PurchasingPrice], [QuantityForPurchasing], [QuantityForRenting], [RentingPrice], [RimSize]) VALUES (18, 3, N'SEAT', N'Azul', N'lsdjf', N'sldfkj', N'gasolina', 6, N'nono', CAST(70.00 AS Decimal(10, 2)), 700, 70, 700, 50)
SET IDENTITY_INSERT [dbo].[Cars] OFF