USE [WebSocketAMR]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 04/01/2024 8:47:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[RoomName] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Room] ([RoomName]) VALUES (N'/ocpp1')
INSERT [dbo].[Room] ([RoomName]) VALUES (N'/ocpp')
GO
/****** Object:  StoredProcedure [dbo].[PR_getAllRoom]    Script Date: 04/01/2024 8:47:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<TyDang>
-- Create date: <04/1/2024>
-- Description:	<Get all room>
-- =============================================
CREATE PROCEDURE [dbo].[PR_getAllRoom]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT RoomName from Room
END
GO
