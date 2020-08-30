namespace POS.Domain.Enums
{
    //[LocalizationEnum(typeof(Enums))]
    //public enum DepartmentType
    //{
    //    Head = 1,
    //    Department = 2,
    //    Section = 3,
    //    Unit = 4
    //}
    public enum PersonType
    {
        Customer = 1,
        Supplier = 2,
        CustomerSupplier = 3
    }

    public enum TransactionType
    {
        Purchase = 1,
        Sale = 2,
        PurchaseBack = 3,
        SaleBack = 4
    }
    public enum PaymentMethod
    {
        Cash = 1,
        Later = 2,
        Visa = 3
    };

    public enum LoanType
    {
        Cheque = 1,
        Loan = 2
    };
    public enum PointType
    {
        Store = 1,
        Market = 2
    }
    public enum BankTransactionTypes : byte
    {
        Deposit = 1,
        WithDrawal = 2
    }
    public enum Operation
    {
        Put = 1,
        Take = 2
    }
    public enum IncomeAccountType
    {
        Sales = 1,
        Installment = 2,
        PartnerSeed = 3,
        PurchaesBack = 4
    }
}

