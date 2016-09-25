create trigger
CASH_TRANS__AI ON CashTransactions
 after insert 
 as   BEGIN
  DECLARE @id int
  DECLARE @number int
  DECLARE @year nvarchar(4)
    SELECT @id = t.Id,@year = DATEPART(YEAR,t.Date) FROM CashTransactions t WHERE t.Id = @@identity;
    SELECT @number = MAX(t.Number) FROM CashTransactions t WHERE DATEPART(YEAR,t.Date) = @year;

    IF @number IS NULL
      SET @number = 1
    ELSE
      SET @number = @number + 1;

    UPDATE CashTransactions
    SET number = @number
    WHERE id = @id;
  END;
  Go
--credit
create trigger
CREDIT_TRANS__AI ON CreditTransactions
 after insert 
 as   BEGIN
  DECLARE @id int
  DECLARE @number int
  DECLARE @year nvarchar(4)
    SELECT @id = t.Id,@year = DATEPART(YEAR,t.Date) FROM CreditTransactions t WHERE t.Id = @@identity;
    SELECT @number = MAX(t.Number) FROM CreditTransactions t WHERE DATEPART(YEAR,t.Date) = @year;

    IF @number IS NULL
      SET @number = 1
    ELSE
      SET @number = @number + 1;

    UPDATE CreditTransactions
    SET number = @number
    WHERE id = @id;
  END
  go
  --commission
  create trigger
COMMISSION_TRANS__AI ON CommissionTransactions
 after insert 
 as   BEGIN
  DECLARE @id int
  DECLARE @number int
  DECLARE @year nvarchar(4)
    SELECT @id = t.Id,@year = DATEPART(YEAR,t.Date) FROM CommissionTransactions t WHERE t.Id = @@identity;
    SELECT @number = MAX(t.Number) FROM CommissionTransactions t WHERE DATEPART(YEAR,t.Date) = @year;

    IF @number IS NULL
      SET @number = 1
    ELSE
      SET @number = @number + 1;

    UPDATE CommissionTransactions
    SET number = @number
    WHERE id = @id;
  END
  go
  --cards trancport
  create trigger
    CARDS_TRANC__AI ON CardTrancports
  after insert 
 as   BEGIN
  DECLARE @id int
  DECLARE @number int
  DECLARE @year nvarchar(4)
    SELECT @id = t.Id,@year = DATEPART(YEAR,t.DateStart) FROM CardTrancports t WHERE t.Id = @@identity;
    SELECT @number = MAX(t.Number) FROM CardTrancports t WHERE DATEPART(YEAR,t.DateStart) = @year;

    IF @number IS NULL
      SET @number = 1
    ELSE
      SET @number = @number + 1;

    UPDATE CardTrancports
    SET number = @number
    WHERE id = @id;
  END
  go