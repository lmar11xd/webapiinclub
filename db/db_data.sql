USE [challengen5]
GO
SET IDENTITY_INSERT [dbo].[ICUsers] ON 

INSERT [dbo].[ICUsers] ([Id], [Username], [PasswordHash], [PasswordSalt], [AddedOn], [ModifiedOn]) VALUES (1, N'lmar11aa', 0x70, 0x70, CAST(N'2022-06-10T23:22:09.997' AS DateTime), CAST(N'2022-06-11T00:53:33.627' AS DateTime))
INSERT [dbo].[ICUsers] ([Id], [Username], [PasswordHash], [PasswordSalt], [AddedOn], [ModifiedOn]) VALUES (3, N'dark', 0xBD209D7D7AE2ABD28AD96DD5CBC04249A46718A3632758B50550592423D93E3807DE451A80D06219AA25CD7D0F39933FAEB69676DBE05C0138762184700D519F, 0x4190B3D70CFE165C274EEE2D66FAF7E050E10BF9B582F6F2C2BF215E2D3CA685D828DF9321104276BD02ABB2932F793EB3D850036A3E73AAFB092285752CECEB0A8FB5418F20C93CE9B8CE8F952EC817A0E4E7145A1A034655784E3D3FFF8C5D4386CFBD17613C5EB89331087E7D97002C4CDB27620041F4645F5CF5B79B2B3C, CAST(N'2022-06-11T00:59:56.250' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[ICUsers] OFF
GO
SET IDENTITY_INSERT [dbo].[ICOrders] ON 

INSERT [dbo].[ICOrders] ([Id], [UserId], [AddedOn], [ModifiedOn]) VALUES (1, 1, CAST(N'2022-06-11T02:06:04.543' AS DateTime), CAST(N'2022-06-11T06:12:52.527' AS DateTime))
INSERT [dbo].[ICOrders] ([Id], [UserId], [AddedOn], [ModifiedOn]) VALUES (3, 3, CAST(N'2022-06-11T02:06:19.157' AS DateTime), NULL)
INSERT [dbo].[ICOrders] ([Id], [UserId], [AddedOn], [ModifiedOn]) VALUES (4, 1, CAST(N'2022-06-11T02:10:56.217' AS DateTime), NULL)
INSERT [dbo].[ICOrders] ([Id], [UserId], [AddedOn], [ModifiedOn]) VALUES (5, 1, CAST(N'2022-06-11T04:35:52.220' AS DateTime), CAST(N'2022-06-11T04:39:39.727' AS DateTime))
INSERT [dbo].[ICOrders] ([Id], [UserId], [AddedOn], [ModifiedOn]) VALUES (10, 3, CAST(N'2022-06-11T05:39:53.927' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[ICOrders] OFF
GO
SET IDENTITY_INSERT [dbo].[ICProducts] ON 

INSERT [dbo].[ICProducts] ([Id], [Name], [Description], [Price], [AddedOn], [ModifiedOn]) VALUES (1, N'Lámpara', N'Lámpara eléctrica', CAST(130.0000 AS Decimal(18, 4)), CAST(N'2022-06-10T21:20:03.197' AS DateTime), CAST(N'2022-06-11T01:25:18.647' AS DateTime))
INSERT [dbo].[ICProducts] ([Id], [Name], [Description], [Price], [AddedOn], [ModifiedOn]) VALUES (2, N'Lapicero', N'Lapicero de tinta azul', CAST(2.0000 AS Decimal(18, 4)), CAST(N'2022-06-11T01:20:21.237' AS DateTime), NULL)
INSERT [dbo].[ICProducts] ([Id], [Name], [Description], [Price], [AddedOn], [ModifiedOn]) VALUES (3, N'Cuaderno', N'Cuaderno cuadriculado Stanford', CAST(10.0000 AS Decimal(18, 4)), CAST(N'2022-06-11T01:21:01.187' AS DateTime), NULL)
INSERT [dbo].[ICProducts] ([Id], [Name], [Description], [Price], [AddedOn], [ModifiedOn]) VALUES (4, N'Silla de Oficina', N'Silla giratoria de color negro', CAST(450.0000 AS Decimal(18, 4)), CAST(N'2022-06-11T01:22:37.563' AS DateTime), NULL)
INSERT [dbo].[ICProducts] ([Id], [Name], [Description], [Price], [AddedOn], [ModifiedOn]) VALUES (5, N'Escritorio', N'Escritorio de 1.5mx60cm', CAST(500.0000 AS Decimal(18, 4)), CAST(N'2022-06-11T01:23:28.050' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[ICProducts] OFF
GO
SET IDENTITY_INSERT [dbo].[ICOrderProducts] ON 

INSERT [dbo].[ICOrderProducts] ([Id], [OrderId], [ProductId], [AddedOn], [ModifiedOn]) VALUES (4, 1, 4, CAST(N'2022-06-11T02:08:47.530' AS DateTime), NULL)
INSERT [dbo].[ICOrderProducts] ([Id], [OrderId], [ProductId], [AddedOn], [ModifiedOn]) VALUES (5, 1, 5, CAST(N'2022-06-11T02:08:47.533' AS DateTime), NULL)
INSERT [dbo].[ICOrderProducts] ([Id], [OrderId], [ProductId], [AddedOn], [ModifiedOn]) VALUES (9, 3, 3, CAST(N'2022-06-11T02:11:05.423' AS DateTime), NULL)
INSERT [dbo].[ICOrderProducts] ([Id], [OrderId], [ProductId], [AddedOn], [ModifiedOn]) VALUES (10, 3, 5, CAST(N'2022-06-11T02:11:05.423' AS DateTime), NULL)
INSERT [dbo].[ICOrderProducts] ([Id], [OrderId], [ProductId], [AddedOn], [ModifiedOn]) VALUES (11, 3, 4, CAST(N'2022-06-11T02:11:05.427' AS DateTime), NULL)
INSERT [dbo].[ICOrderProducts] ([Id], [OrderId], [ProductId], [AddedOn], [ModifiedOn]) VALUES (12, 4, 5, CAST(N'2022-06-11T02:11:28.150' AS DateTime), NULL)
INSERT [dbo].[ICOrderProducts] ([Id], [OrderId], [ProductId], [AddedOn], [ModifiedOn]) VALUES (14, 5, 3, CAST(N'2022-06-11T04:35:52.267' AS DateTime), NULL)
INSERT [dbo].[ICOrderProducts] ([Id], [OrderId], [ProductId], [AddedOn], [ModifiedOn]) VALUES (15, 5, 4, CAST(N'2022-06-11T04:39:39.783' AS DateTime), NULL)
INSERT [dbo].[ICOrderProducts] ([Id], [OrderId], [ProductId], [AddedOn], [ModifiedOn]) VALUES (16, 10, 1, CAST(N'2022-06-11T05:39:53.927' AS DateTime), NULL)
INSERT [dbo].[ICOrderProducts] ([Id], [OrderId], [ProductId], [AddedOn], [ModifiedOn]) VALUES (17, 10, 2, CAST(N'2022-06-11T05:39:53.927' AS DateTime), NULL)
INSERT [dbo].[ICOrderProducts] ([Id], [OrderId], [ProductId], [AddedOn], [ModifiedOn]) VALUES (23, 1, 2, CAST(N'2022-06-11T06:12:52.530' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[ICOrderProducts] OFF
GO
