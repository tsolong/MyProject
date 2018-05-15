/*select * from tl_shop_user

select * from tl_shop_user where 
Area =19 and 
AreaSub = 387 and 
FoodSeries like '%{1}%' and 
FoodSeriesSub like '%{8}%' and 
Consume = 1 and 
[Level] = 2 and 
Balcony = 1 and 
Takeaway = 1 and 
Card = 0 and 
Park = 1;
*/



GO

drop function SearchShopFunction

GO

create function SearchShopFunction(@StrSql nvarchar(1000), @NewSql nvarchar(100))
returns nvarchar(1000)
as
	begin
		if @StrSql = ''
			begin
				set @StrSql = ' where ' + @NewSql
			end
		else
			begin
				set @StrSql = @StrSql + ' and '  + @NewSql
			end
		return @StrSql
	end
GO

drop procedure SearchShop

GO

create procedure SearchShop
	@Area int = 0,
	@AreaSub int = 0,
	@FoodSeries nvarchar(10) = '',
	@FoodSeriesSub nvarchar(10) = '',
	@Consume int = 0,
	@Level int = 0,
	@Balcony int = 0,
	@Takeaway int = 0,
	@Card int = 0,
	@Park int = 0
as
set nocount on
declare @StrSQL nvarchar(4000)
set @StrSQL = ''
	
	if @Area != 0
		begin
			set @StrSQL = (select dbo.SearchShopFunction(@StrSQL, 'Area =' + Str(@Area)))
		end

	if @AreaSub != 0
		begin
			set @StrSQL = (select dbo.SearchShopFunction(@StrSQL, 'AreaSub =' + Str(@AreaSub)))
		end
	
	if @FoodSeries != ''
		begin
			set @StrSQL = (select dbo.SearchShopFunction(@StrSQL, 'FoodSeries like ''%' + @FoodSeries + '%'''))
		end

	if @FoodSeriesSub != ''
		begin
			set @StrSQL = (select dbo.SearchShopFunction(@StrSQL, 'FoodSeriesSub like ''%' + @FoodSeriesSub + '%'''))
		end

	if @Consume != 0
		begin
			set @StrSQL = (select dbo.SearchShopFunction(@StrSQL, 'Consume =' + Str(@Consume)))
		end

	if @Level != 0
		begin
			set @StrSQL = (select dbo.SearchShopFunction(@StrSQL, 'Level =' + Str(@Level)))
		end

	if @Balcony != 0
		begin
			set @StrSQL = (select dbo.SearchShopFunction(@StrSQL, 'Balcony =' + Str(@Balcony)))
		end

	if @Takeaway != 0
		begin
			set @StrSQL = (select dbo.SearchShopFunction(@StrSQL, 'Takeaway =' + Str(@Takeaway)))
		end

	if @Card != 0
		begin
			set @StrSQL = (select dbo.SearchShopFunction(@StrSQL, 'Card =' + Str(@Card)))
		end

	if @Park != 0
		begin
			set @StrSQL = (select dbo.SearchShopFunction(@StrSQL, 'Park =' + Str(@Park)))
		end

	if @StrSQL != ''
		begin
			set @StrSQL = 'select * from TL_Shop_User' + @StrSQL + ' and  [IsOnline] = 1'
		end
	else
		begin
			set @StrSQL = 'select * from TL_Shop_User where  [IsOnline] = 1'
		end
	
execute (@StrSQL)

print @StrSql

GO

execute SearchShop 10, 20 ,'{10}','{123}'


select * from TL_Shop_User where Area =        10 and AreaSub =        20 and FoodSeries like '%{10}%'




select * from tl_shop_user

select * from tl_shop_photo