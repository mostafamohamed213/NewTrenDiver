Create View [dbo].[VW_Channel] 
as
select Id,[Name],Logo,[Description],Bio,BankAccountNumber,channelType,
BankId,UserId,CategoryId from [dbo].[Channel]