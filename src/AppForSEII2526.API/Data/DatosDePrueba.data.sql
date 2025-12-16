/* Script de Poblacion de Datos - Sin tildes ni caracteres especiales
   IDs simplificados (1, 2, 3...) y fechas formato YYYY-MM-DD.
*/

-- 1. USUARIOS (ApplicationUsers)
INSERT INTO [dbo].[AspNetUsers] (
    [Id], [Name], [Surname], [ClientPhoneNumber], [UserName], [NormalizedUserName],
    [Email], [NormalizedEmail], [EmailConfirmed],
    [PasswordHash], [SecurityStamp], [ConcurrencyStamp],
    [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled],
    [LockoutEnd], [LockoutEnabled], [AccessFailedCount]
)
VALUES
(
    N'1',                                            -- Id: 1 (Carlos)
    N'Carlos', N'Garcia', N'+34 600111222',
    N'carlosg', N'CARLOSG',
    N'carlos.garcia@gmail.com', N'CARLOS.GARCIA@GMAIL.COM', 1,
    N'AQAAAAEAACcQAAAAEJw...', N'7f1b0d0a...', N'c3b6b9a2...', N'+34 600111222', 1, 0, NULL, 1, 0
),
(
    N'2',                                            -- Id: 2 (Laura)
    N'Laura', N'Martinez', N'+34 655888999',
    N'lauram', N'LAURAM',
    N'laura.martinez@hotmail.com', N'LAURA.MARTINEZ@HOTMAIL.COM', 1,
    N'AQAAAAEAACcQAAAAEJw...', N'9a8b7c6d...', N'0f1e2d3c...', N'+34 655888999', 1, 0, NULL, 1, 0
),
(
    N'3',                                            -- Id: 3 (David)
    N'David', N'Sanchez', N'+34 611444555',
    N'davids', N'DAVIDS',
    N'david.sanchez@empresa.com', N'DAVID.SANCHEZ@EMPRESA.COM', 1,
    N'AQAAAAEAACcQAAAAEJw...', N'1a2b3c4d...', N'5p6q7r8s...', N'+34 611444555', 0, 0, NULL, 1, 0
);

-- 2. MODELOS (Models)
SET IDENTITY_INSERT [dbo].[Models] ON
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (1, N'TOYOTA')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (2, N'KIA')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (3, N'BMW')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (4, N'VOLKSWAGEN')
SET IDENTITY_INSERT [dbo].[Models] OFF
GO

-- 3. MANTENIMIENTOS (Maintenances)
SET IDENTITY_INSERT [dbo].[Maintenances] ON
INSERT INTO [dbo].[Maintenances] ([Id], [Name], [NumberOfDays], [Price]) VALUES (1, N'Cambio de Aceite', 1, CAST(65.50 AS Decimal(18, 2)))
INSERT INTO [dbo].[Maintenances] ([Id], [Name], [NumberOfDays], [Price]) VALUES (2, N'Revision ITV', 2, CAST(45.00 AS Decimal(18, 2)))
INSERT INTO [dbo].[Maintenances] ([Id], [Name], [NumberOfDays], [Price]) VALUES (3, N'Cambio Kit Distrib.', 4, CAST(450.00 AS Decimal(18, 2)))
INSERT INTO [dbo].[Maintenances] ([Id], [Name], [NumberOfDays], [Price]) VALUES (4, N'Alineacion Ruedas', 1, CAST(40.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Maintenances] OFF
GO

-- 4. TIPOS DE MANTENIMIENTO (MaintenanceTypes)
SET IDENTITY_INSERT [dbo].[MaintenanceTypes] ON
INSERT INTO [dbo].[MaintenanceTypes] ([id], [Type], [MaintenanceId]) VALUES (1, N'Preventivo', 1)
INSERT INTO [dbo].[MaintenanceTypes] ([id], [Type], [MaintenanceId]) VALUES (2, N'Legal', 2)
INSERT INTO [dbo].[MaintenanceTypes] ([id], [Type], [MaintenanceId]) VALUES (3, N'Correctivo', 3)
INSERT INTO [dbo].[MaintenanceTypes] ([id], [Type], [MaintenanceId]) VALUES (4, N'Mecanica Rapida', 4)
SET IDENTITY_INSERT [dbo].[MaintenanceTypes] OFF
GO

-- 5. COCHES (Cars)
SET IDENTITY_INSERT [dbo].[Cars] ON
INSERT INTO [dbo].[Cars] ([Id], [ModelId], [CarClass], [Color], [Description], [EngDispacement], [FuelType], [MaintenanceTypesid], [Manufacturer], [PurchasingPrice], [QuantityForPurchasing], [QuantityForRenting], [RentingPrice], [RimSize]) 
VALUES (1, 1, N'Utilitario', N'Blanco', N'Toyota Yaris Hibrido.', N'1.5L', N'Hibrido', 1, N'TOYOTA', 21500.00, 15, 10, 35.00, 16)

INSERT INTO [dbo].[Cars] ([Id], [ModelId], [CarClass], [Color], [Description], [EngDispacement], [FuelType], [MaintenanceTypesid], [Manufacturer], [PurchasingPrice], [QuantityForPurchasing], [QuantityForRenting], [RentingPrice], [RimSize]) 
VALUES (2, 2, N'SUV', N'Gris', N'Kia Sportage GT Line.', N'1.6L', N'Diesel', 2, N'KIA', 32900.00, 8, 5, 85.00, 19)

INSERT INTO [dbo].[Cars] ([Id], [ModelId], [CarClass], [Color], [Description], [EngDispacement], [FuelType], [MaintenanceTypesid], [Manufacturer], [PurchasingPrice], [QuantityForPurchasing], [QuantityForRenting], [RentingPrice], [RimSize]) 
VALUES (3, 3, N'Berlina', N'Azul', N'BMW 320d Pack M.', N'2.0L', N'Diesel', 3, N'BMW', 48500.00, 4, 2, 120.00, 18)

INSERT INTO [dbo].[Cars] ([Id], [ModelId], [CarClass], [Color], [Description], [EngDispacement], [FuelType], [MaintenanceTypesid], [Manufacturer], [PurchasingPrice], [QuantityForPurchasing], [QuantityForRenting], [RentingPrice], [RimSize]) 
VALUES (4, 4, N'Compacto', N'Rojo', N'VW Golf 8 Life.', N'1.5 TSI', N'Gasolina', 4, N'VOLKSWAGEN', 26000.00, 12, 8, 55.00, 17)
SET IDENTITY_INSERT [dbo].[Cars] OFF
GO

-- 6. COMPRAS (Purchases)
SET IDENTITY_INSERT [dbo].[Purchases] ON
-- Compra 1
INSERT INTO [dbo].[Purchases] ([Id], [DeliveryCarDealer], [PurchasingDate], [PurchasingPrice], [ApplicationUserId], [PaymentMethod]) 
VALUES (1, N'Toyota Madrid Norte', '2023-11-20', 21500.00, 1, 0)

-- Compra 2
INSERT INTO [dbo].[Purchases] ([Id], [DeliveryCarDealer], [PurchasingDate], [PurchasingPrice], [ApplicationUserId], [PaymentMethod]) 
VALUES (2, N'Castellana Wagen', '2024-01-15', 26000.00, 3, 3)

-- Compra 3
INSERT INTO [dbo].[Purchases] ([Id], [DeliveryCarDealer], [PurchasingDate], [PurchasingPrice], [ApplicationUserId], [PaymentMethod]) 
VALUES (3, N'BMW Madrid Las Tablas', '2024-04-20', 48500.00, 2, 1)
SET IDENTITY_INSERT [dbo].[Purchases] OFF
GO

-- 7. DETALLE COMPRA (PurchaseItem)
INSERT INTO [dbo].[PurchaseItem] ([PurchaseId], [CarId], [Quantity]) VALUES (1, 1, 1)
INSERT INTO [dbo].[PurchaseItem] ([PurchaseId], [CarId], [Quantity]) VALUES (2, 4, 1)
INSERT INTO [dbo].[PurchaseItem] ([PurchaseId], [CarId], [Quantity]) VALUES (3, 3, 1)
GO

-- 8. ALQUILERES (Rentals)
SET IDENTITY_INSERT [dbo].[Rentals] ON
-- Alquiler 1
INSERT INTO [dbo].[Rentals] ([Id], [TotalPrice], [DeliveryCarDealer], [ApplicationUserId], [EndDate], [RentingDate], [StartDate], [PaymentMethod]) 
VALUES (1, 170.00, N'Oficina Atocha Renfe', 2, '2024-02-12', '2024-02-01', '2024-02-10', 1)

-- Alquiler 2
INSERT INTO [dbo].[Rentals] ([Id], [TotalPrice], [DeliveryCarDealer], [ApplicationUserId], [EndDate], [RentingDate], [StartDate], [PaymentMethod]) 
VALUES (2, 165.00, N'Oficina Aeropuerto T4', 3, '2024-05-05', '2024-05-01', '2024-05-02', 0)
SET IDENTITY_INSERT [dbo].[Rentals] OFF
GO

-- 9. DETALLE ALQUILER (RentalItem)
INSERT INTO [dbo].[RentalItem] ([RentalId], [CarId], [Quantity]) VALUES (1, 2, 1)
INSERT INTO [dbo].[RentalItem] ([RentalId], [CarId], [Quantity]) VALUES (2, 4, 1)
GO

-- 10. RESERVAS (Bookings)
SET IDENTITY_INSERT [dbo].[Bookings] ON
-- Reserva 1
INSERT INTO [dbo].[Bookings] ([Id], [ApplicationUserId], [ClientAddress], [TotalPrice], [Date], [PaymentMethod]) 
VALUES (1, 1, N'Calle Alcala 45, Madrid', 65.50, '2024-03-05', 0)

-- Reserva 2
INSERT INTO [dbo].[Bookings] ([Id], [ApplicationUserId], [ClientAddress], [TotalPrice], [Date], [PaymentMethod]) 
VALUES (2, 2, N'Av. de America 12, Madrid', 45.00, '2024-06-15', 1)
SET IDENTITY_INSERT [dbo].[Bookings] OFF
GO

-- 11. DETALLE RESERVA (BookingItem)
INSERT INTO [dbo].[BookingItem] ([BookingId], [MaintenanceId], [Comment]) VALUES (1, 1, N'Primera revision tras rodaje.')
INSERT INTO [dbo].[BookingItem] ([BookingId], [MaintenanceId], [Comment]) VALUES (2, 2, N'Necesito pasar la ITV urgente.')
GO

-- 12. RESEÑAS (Reviews)
SET IDENTITY_INSERT [dbo].[Reviews] ON
-- Resena 1
INSERT INTO [dbo].[Reviews] ([Id], [ApplicationUserId], [Country], [Created], [DriverType]) 
VALUES (1, 2, N'Espana', '2024-02-13', 0)

-- Resena 2
INSERT INTO [dbo].[Reviews] ([Id], [ApplicationUserId], [Country], [Created], [DriverType]) 
VALUES (2, 1, N'Espana', '2024-01-10', 1)
SET IDENTITY_INSERT [dbo].[Reviews] OFF
GO

-- 13. DETALLE RESEÑA (ReviewItem)
INSERT INTO [dbo].[ReviewItem] ([ReviewId], [CarId], [Rating], [Description]) VALUES (1, 2, 5, N'Me encanto el coche.')
INSERT INTO [dbo].[ReviewItem] ([ReviewId], [CarId], [Rating], [Description]) VALUES (2, 1, 4, N'Muy buen consumo.')
GO