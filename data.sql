USE [MNLibrary]
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (2, N'User')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([UserID], [UserName], [Email], [Password], [RoleID]) VALUES (1, N'admin', N'admin@library.com', N'123', 1)
INSERT [dbo].[Accounts] ([UserID], [UserName], [Email], [Password], [RoleID]) VALUES (10, N'dotritrong', N'dotritrong@gmail.com', N'123', 2)
INSERT [dbo].[Accounts] ([UserID], [UserName], [Email], [Password], [RoleID]) VALUES (11, N'test2', N'test2@gmail.com', N'123', 2)
INSERT [dbo].[Accounts] ([UserID], [UserName], [Email], [Password], [RoleID]) VALUES (16, N'user', N'user@gmail.com', N'123', 2)
INSERT [dbo].[Accounts] ([UserID], [UserName], [Email], [Password], [RoleID]) VALUES (17, N'test4', N'test4@gmail.com', N'123', 2)
INSERT [dbo].[Accounts] ([UserID], [UserName], [Email], [Password], [RoleID]) VALUES (21, N'anhpark1', N'anhpark@gmail.com', N'123', 2)
INSERT [dbo].[Accounts] ([UserID], [UserName], [Email], [Password], [RoleID]) VALUES (22, N'demo1', N'demo1@gmail.com', N'123', 2)
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Profiles] ON 

INSERT [dbo].[Profiles] ([ProfileID], [UserID], [FullName], [DateOfBirth], [ReaderCode], [Address], [PhoneNumber]) VALUES (1, 10, N'', NULL, N'HE0001', NULL, NULL)
INSERT [dbo].[Profiles] ([ProfileID], [UserID], [FullName], [DateOfBirth], [ReaderCode], [Address], [PhoneNumber]) VALUES (2, 11, N'Do Tri Trong', CAST(N'2003-07-13' AS Date), N'HE0002', N'Ha Noi', N'0968736913')
INSERT [dbo].[Profiles] ([ProfileID], [UserID], [FullName], [DateOfBirth], [ReaderCode], [Address], [PhoneNumber]) VALUES (3, 16, N'', NULL, N'HE0003', NULL, NULL)
INSERT [dbo].[Profiles] ([ProfileID], [UserID], [FullName], [DateOfBirth], [ReaderCode], [Address], [PhoneNumber]) VALUES (4, 17, N'Do Tri Hoang', CAST(N'2003-12-01' AS Date), N'HE0004', N'HN', N'0968736913')
INSERT [dbo].[Profiles] ([ProfileID], [UserID], [FullName], [DateOfBirth], [ReaderCode], [Address], [PhoneNumber]) VALUES (5, 21, N'', NULL, N'HE0005', NULL, NULL)
INSERT [dbo].[Profiles] ([ProfileID], [UserID], [FullName], [DateOfBirth], [ReaderCode], [Address], [PhoneNumber]) VALUES (6, 22, N'', NULL, N'HE0006', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Profiles] OFF
GO
SET IDENTITY_INSERT [dbo].[Authors] ON 

INSERT [dbo].[Authors] ([AuthorID], [AuthorName]) VALUES (1, N'J.K. Rowling')
INSERT [dbo].[Authors] ([AuthorID], [AuthorName]) VALUES (2, N'George R.R. Martin')
INSERT [dbo].[Authors] ([AuthorID], [AuthorName]) VALUES (3, N'J.R.R. Tolkien')
INSERT [dbo].[Authors] ([AuthorID], [AuthorName]) VALUES (4, N'Craig Foster')
INSERT [dbo].[Authors] ([AuthorID], [AuthorName]) VALUES (5, N'Brianna Madia')
INSERT [dbo].[Authors] ([AuthorID], [AuthorName]) VALUES (6, N'Lucas Buchholz')
INSERT [dbo].[Authors] ([AuthorID], [AuthorName]) VALUES (7, N'Kousuke Oono')
INSERT [dbo].[Authors] ([AuthorID], [AuthorName]) VALUES (8, N'Marcela Fuentes')
INSERT [dbo].[Authors] ([AuthorID], [AuthorName]) VALUES (9, N'Alan Murrin')
INSERT [dbo].[Authors] ([AuthorID], [AuthorName]) VALUES (10, N'Jen Soriano')
INSERT [dbo].[Authors] ([AuthorID], [AuthorName]) VALUES (11, N'Rufi Thorpe')
INSERT [dbo].[Authors] ([AuthorID], [AuthorName]) VALUES (12, N'Anthony Fauci')
INSERT [dbo].[Authors] ([AuthorID], [AuthorName]) VALUES (13, N'J.K. Rowling')
INSERT [dbo].[Authors] ([AuthorID], [AuthorName]) VALUES (14, N'George R.R. Martin')
INSERT [dbo].[Authors] ([AuthorID], [AuthorName]) VALUES (15, N'J.R.R. Tolkien')
SET IDENTITY_INSERT [dbo].[Authors] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (1, N'Fantasy')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (2, N'Science Fiction')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (3, N'Mystery')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (4, N'Animals')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (5, N'Adventure')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (6, N'Environmental Science')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (7, N'Manga')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (8, N'Family Life')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (9, N'Literary')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (10, N'Women')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (11, N'Neuroscience')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (12, N'Political')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (13, N'Fantasy')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (14, N'Science Fiction')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (15, N'Mystery')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [AuthorID], [Image], [CategoryID], [CreateDate], [UpdateDate], [Quantity], [Status]) VALUES (1, N'Harry Potter and the Philosopher''s Stone', N'9780747532699', 1, N'harry_potter_image.jpg', 1, CAST(N'2024-07-06T17:16:47.030' AS DateTime), CAST(N'2024-07-20T08:44:29.747' AS DateTime), 2, 0)
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [AuthorID], [Image], [CategoryID], [CreateDate], [UpdateDate], [Quantity], [Status]) VALUES (2, N'A Game of Thrones', N'9780553573404', 2, N'game_of_thrones_image.jpg', 3, CAST(N'2024-07-06T17:16:47.030' AS DateTime), CAST(N'2024-08-20T20:00:04.303' AS DateTime), 7, 0)
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [AuthorID], [Image], [CategoryID], [CreateDate], [UpdateDate], [Quantity], [Status]) VALUES (3, N'The Hobbit', N'9780547928227', 3, N'hobbit_image.jpg', 1, CAST(N'2024-07-06T17:16:47.030' AS DateTime), CAST(N'2024-07-18T01:55:04.980' AS DateTime), 1, 0)
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [AuthorID], [Image], [CategoryID], [CreateDate], [UpdateDate], [Quantity], [Status]) VALUES (9, N'The Lightning Thief', N'9781368051477', 3, N'percy_jackson_and _the_olympians.jpg', 1, CAST(N'2024-07-06T17:16:47.030' AS DateTime), CAST(N'2024-07-06T17:46:07.060' AS DateTime), 3, 0)
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [AuthorID], [Image], [CategoryID], [CreateDate], [UpdateDate], [Quantity], [Status]) VALUES (10, N'Finding the Wild in a Tame World', N'9780063289024', 4, N'9780063289024.jpg', 4, CAST(N'2024-07-06T17:16:47.030' AS DateTime), CAST(N'2024-07-18T02:14:21.257' AS DateTime), 0, 0)
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [AuthorID], [Image], [CategoryID], [CreateDate], [UpdateDate], [Quantity], [Status]) VALUES (12, N'Never Leave the Dogs Behind: A Memoir', N'9780063316096', 5, N'9780063316096.jpg', 5, CAST(N'2024-07-06T17:16:47.030' AS DateTime), CAST(N'2024-07-06T17:48:11.147' AS DateTime), 2, 0)
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [AuthorID], [Image], [CategoryID], [CreateDate], [UpdateDate], [Quantity], [Status]) VALUES (13, N'Kogi Wisdom for a Good Life and Thriving Earth', N'9780063329904', 6, N'9780063329904.jpg', 6, CAST(N'2024-07-06T17:16:47.030' AS DateTime), CAST(N'2024-07-06T17:46:23.950' AS DateTime), 2, 0)
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [AuthorID], [Image], [CategoryID], [CreateDate], [UpdateDate], [Quantity], [Status]) VALUES (14, N'The Way of the Househusband, Vol. 1', N'9781974709403', 7, N'9781974709403.jpg', 7, CAST(N'2024-07-06T17:16:47.030' AS DateTime), CAST(N'2024-07-06T17:46:00.627' AS DateTime), 1, 0)
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [AuthorID], [Image], [CategoryID], [CreateDate], [UpdateDate], [Quantity], [Status]) VALUES (16, N'Malas', N'9780593655788', 8, N'9780593655788.jpg', 9, CAST(N'2024-07-06T17:16:47.030' AS DateTime), CAST(N'2024-07-06T17:45:54.123' AS DateTime), 0, 0)
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [AuthorID], [Image], [CategoryID], [CreateDate], [UpdateDate], [Quantity], [Status]) VALUES (17, N'The Coast Road', N'9780063336520', 9, N'9780063336520.jpg', 10, CAST(N'2024-07-06T17:16:47.030' AS DateTime), CAST(N'2024-07-18T01:55:35.617' AS DateTime), 10, 0)
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [AuthorID], [Image], [CategoryID], [CreateDate], [UpdateDate], [Quantity], [Status]) VALUES (18, N'Essays on Heritage and Healing', N'9780063230132', 10, N'9780063230132.jpg', 11, CAST(N'2024-07-06T17:39:28.390' AS DateTime), CAST(N'2024-10-14T10:16:03.283' AS DateTime), 4, 0)
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [AuthorID], [Image], [CategoryID], [CreateDate], [UpdateDate], [Quantity], [Status]) VALUES (19, N'Margo''s Got Money Troubles', N'9780063356580', 11, N'9780063356580.jpg', 10, CAST(N'2024-07-08T11:27:11.400' AS DateTime), CAST(N'2024-07-09T01:50:30.047' AS DateTime), 8, 0)
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [AuthorID], [Image], [CategoryID], [CreateDate], [UpdateDate], [Quantity], [Status]) VALUES (20, N'On Call: A Doctor''s Journey in Public Service', N'9780593657478', 12, N'9798217014309.JPG', 12, CAST(N'2024-07-08T11:29:07.397' AS DateTime), CAST(N'2024-07-08T11:30:47.900' AS DateTime), 3, 0)
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [AuthorID], [Image], [CategoryID], [CreateDate], [UpdateDate], [Quantity], [Status]) VALUES (23, N'Brick by Brick', N'9780525517306', 9, N'9780525517306.jpg', 5, CAST(N'2024-07-16T09:52:03.450' AS DateTime), CAST(N'2024-07-20T04:03:13.137' AS DateTime), 8, -1)
INSERT [dbo].[Books] ([BookID], [Title], [ISBN], [AuthorID], [Image], [CategoryID], [CreateDate], [UpdateDate], [Quantity], [Status]) VALUES (24, N'Where You See Yourself', N'9781338813838', 6, N'9781338813838.jpg', 10, CAST(N'2024-07-16T09:53:06.467' AS DateTime), CAST(N'2024-07-18T08:30:58.247' AS DateTime), 6, -1)
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
SET IDENTITY_INSERT [dbo].[BorrowRecord] ON 

INSERT [dbo].[BorrowRecord] ([BorrowID], [ISBN], [UserID], [BorrowDate], [DueDate], [ReturnDate], [Quantity], [Notes], [Status]) VALUES (17, N'9780547928227', 11, CAST(N'2024-07-20' AS Date), CAST(N'2024-07-25' AS Date), NULL, 1, N'', -2)
INSERT [dbo].[BorrowRecord] ([BorrowID], [ISBN], [UserID], [BorrowDate], [DueDate], [ReturnDate], [Quantity], [Notes], [Status]) VALUES (20, N'9780063356580', 21, CAST(N'2024-08-20' AS Date), CAST(N'2024-08-23' AS Date), NULL, 1, N'', -2)
SET IDENTITY_INSERT [dbo].[BorrowRecord] OFF
GO
