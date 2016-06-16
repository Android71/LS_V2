USE [LightSystemV1]
GO
SET IDENTITY_INSERT [dbo].[ControlSpaces] ON 

INSERT [dbo].[ControlSpaces] ([Id], [Name], [IsActive]) VALUES (1, N'ArtNet', 1)
INSERT [dbo].[ControlSpaces] ([Id], [Name], [IsActive]) VALUES (2, N'DMX', 0)
INSERT [dbo].[ControlSpaces] ([Id], [Name], [IsActive]) VALUES (3, N'NooLite', 1)
INSERT [dbo].[ControlSpaces] ([Id], [Name], [IsActive]) VALUES (4, N'MiLight', 0)
SET IDENTITY_INSERT [dbo].[ControlSpaces] OFF
SET IDENTITY_INSERT [dbo].[Partitions] ON 

INSERT [dbo].[Partitions] ([Id], [Name]) VALUES (1, N'Кухня')
INSERT [dbo].[Partitions] ([Id], [Name]) VALUES (2, N'Прихожая')
INSERT [dbo].[Partitions] ([Id], [Name]) VALUES (3, N'Детская')
SET IDENTITY_INSERT [dbo].[Partitions] OFF
SET IDENTITY_INSERT [dbo].[EnvironmentItems] ON 

INSERT [dbo].[EnvironmentItems] ([Id], [Model], [DeviceType], [Profile], [DotNetType]) VALUES (1, N'AN6USPI', 1, N'<Params>
  <IPAddress Value="2.0.0.2" ChCount="4"/>
  <VirtualIP Value="2.0.0.3" ChCount="2"/>
</Params>', N'LS_Designer_WPF.Model.AN6USPI, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null')
INSERT [dbo].[EnvironmentItems] ([Id], [Model], [DeviceType], [Profile], [DotNetType]) VALUES (2, N'SU111-300', 1, N'<Params HaveDimmer="true" />', NULL)
INSERT [dbo].[EnvironmentItems] ([Id], [Model], [DeviceType], [Profile], [DotNetType]) VALUES (4, N'SU111-500', 1, N'<Params HaveDimmer="true" />', NULL)
INSERT [dbo].[EnvironmentItems] ([Id], [Model], [DeviceType], [Profile], [DotNetType]) VALUES (6, N'PB411', 0, N'<Params>
  <Mode Value="1">
    <EventSource ChannelNo="1">
      <Event Name="DimIn"/>
      <Event Name="On"/>
    </EventSource>
    <EventSource ChannelNo="2">
      <Event Name="DimOut"/>
      <Event Name="Off"/>
    </EventSource>
    <EventSource ChannelNo="3">
      <Event Name="DimIn"/>
      <Event Name="On"/>
    </EventSource>
    <EventSource ChannelNo="4">
      <Event Name="DimOut"/>
      <Event Name="Off"/>
    </EventSource>
  </Mode>
  <Mode Value="2">
    <EventSource ChannelNo="1">
      <Event Name="On/Off Trigger"/>
      <Event Name="Dim In/Out"/>
    </EventSource>
    <EventSource ChannelNo="2">
      <Event Name="On/Off Trigger"/>
      <Event Name="Dim In/Out"/>
    </EventSource>
    <EventSource ChannelNo="3">
      <Event Name="On/Off Trigger"/>
      <Event Name="Dim In/Out"/>
    </EventSource>
    <EventSource ChannelNo="4">
      <Event Name="On/Off Trigger"/>
      <Event Name="Dim In/Out"/>
    </EventSource>
  </Mode>
  <Mode Value="3">
    <EventSource ChannelNo="1">
      <Event Name="Dim In/Out"/>
      <Event Name="On"/>
    </EventSource>
    <EventSource ChannelNo="2">
      <Event Name="On"/>
    </EventSource>
    <EventSource ChannelNo="3">
      <Event Name="Dim In/Out"/>
      <Event Name="On"/>
    </EventSource>
    <EventSource ChannelNo="4">
      <Event Name="On"/>
    </EventSource>
  </Mode>
  <Mode Value="4">
    <EventSource ChannelNo="1">
      <Event Name="On"/>
    </EventSource>
    <EventSource ChannelNo="2">
      <Event Name="On"/>
    </EventSource>
    <EventSource ChannelNo="3">
      <Event Name="On"/>
    </EventSource>
    <EventSource ChannelNo="4">
      <Event Name="On"/>
    </EventSource>
  </Mode>
</Params>', NULL)
INSERT [dbo].[EnvironmentItems] ([Id], [Model], [DeviceType], [Profile], [DotNetType]) VALUES (7, N'SD111-180', 1, N'<Params HaveDimmer="true" />', NULL)
SET IDENTITY_INSERT [dbo].[EnvironmentItems] OFF
SET IDENTITY_INSERT [dbo].[CSEnvItems] ON 

INSERT [dbo].[CSEnvItems] ([Id], [EnvironmentItem_Id], [ControlSpace_Id]) VALUES (1, 1, 1)
INSERT [dbo].[CSEnvItems] ([Id], [EnvironmentItem_Id], [ControlSpace_Id]) VALUES (2, 2, 3)
INSERT [dbo].[CSEnvItems] ([Id], [EnvironmentItem_Id], [ControlSpace_Id]) VALUES (3, 4, 3)
INSERT [dbo].[CSEnvItems] ([Id], [EnvironmentItem_Id], [ControlSpace_Id]) VALUES (4, 6, 3)
INSERT [dbo].[CSEnvItems] ([Id], [EnvironmentItem_Id], [ControlSpace_Id]) VALUES (5, 7, 3)
SET IDENTITY_INSERT [dbo].[CSEnvItems] OFF
