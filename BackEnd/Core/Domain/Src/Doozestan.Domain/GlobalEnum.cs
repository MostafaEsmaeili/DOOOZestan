using System.ComponentModel;
using System.Runtime.Serialization;

namespace Doozestan.Domain
{
    [DataContract]
    public enum ImportType
    {
        [EnumMember]
        Agnet = 1,
        [EnumMember]
        Manual = 2
    }

    [DataContract]
    public enum LoginStatusEnum
    {
        [EnumMember]
        Ok,
        [EnumMember]
        ExpectationFailed,
        [EnumMember]
        BadRequest,
        [EnumMember]
        MethodNotAllowed
    }


    [DataContract]
    public enum Role
    {
        [EnumMember]
        Customer = 1,
        [EnumMember]
        ContentOperator = 2,
        [EnumMember]
        Admin = 3,
        [EnumMember]
        Inspector = 4,
        [EnumMember]
        Member = 5,
        [EnumMember]
        Trader = 6,
        [EnumMember]
        InsideOperator = 7
    }

    public enum BusinessRuleCode
    {
        Unknwon = 0,
        [Description("Invalid Isin")]
        InvalidIsin = 1,
        [Description("Invalid Product Symbol")]
        InvalidProductSymbol = 2,
        [Description("Invalid Product English Symbol")]
        InvalidProductEnglishSymbol = 3,
        [Description("Invalid National Identification")]
        InvalidNationalIdentification = 4,
        [Description("Invalid Product")]
        InvalidProduct = 5,
        [Description("Invalid Party")]
        InvalidParty = 6,
        [Description("Invalid RoleType")]
        InvalidRoleType = 7,
        [Description("Invalid Economic Id")]
        InvalidEconomicId = 8,
    }

    public enum ProductStatus
    {
        Active = 1,
        InActive = 2
    }

    public enum StockExchange
    {
        [Description("TSE")]
        TSE = 1,
        [Description("OTC")]
        OTC = 2,
        [Description("IME")]
        IME = 3,
        [Description("Irenex")]
        Irenex = 4,
        [Description("Unknown")]
        Unknown = 5
    }

    public enum AllocationType
    {
        [Description("AssetType")]
        Asset = 1,
        [Description("Sector")]
        Sector = 2
    }

    public enum AssetType
    {
        [Description("Equities")]
        Equities = 1,
        [Description("Derivatives")]
        Derivatives = 2,
        [Description("MutualFunds")]
        MutualFunds = 3,
        [Description("AlternativeInvestments")]
        AlternativeInvestments = 4,
        [Description("Bonds")]
        Bonds = 5,
        [Description("Property")]
        Property = 6,
        [Description("Commodities")]
        Commodities = 7,
        [Description("InsurancePolicies")]
        InsurancePolicies = 8,
        [Description("CashAndEquivalents")]
        CashAndEquivalents = 9,
        [Description("Other")]
        Other = 10
    }

    public enum ProductType
    {
        [Description("0001")]
        Stock = 1,
        [Description("0002")]
        Fund = 2,
        [Description("0003")]
        Bond = 3,
        [Description("0004")]
        Option = 4,
        [Description("0005")]
        Index = 5,
        [Description("0006")]
        Currency = 6,
        [Description("0007")]
        Coupon = 7,
        [Description("0008")]
        Right = 8,
        [Description("0009")]
        Property = 9,
        [Description("0010")]
        Other = 10,
        [Description("0011")]
        Warrant = 11,
        [Description("0012")]
        Cash = 12,
        [Description("0013")]
        Furures = 13,
        [Description("0016")]
        NonUnitised = 16
    }

    public enum ProductClassification
    {
        [Description("0001")]
        Share = 1,
        [Description("0002")]
        MutualFund = 2,
        [Description("0003")]
        Bond = 3,
        [Description("0004")]
        Option = 4,
        [Description("0005")]
        Index = 5,
        [Description("0006")]
        Currency = 6,
        [Description("0007")]
        Coupon = 7,
        [Description("0008")]
        Right = 8,
        [Description("0009")]
        RealEstate = 9,
        [Description("0010")]
        Other = 10,
        [Description("0011")]
        Warrant = 11,
        [Description("0012")]
        Cash = 12,
        [Description("0013")]
        Future = 13,
        [Description("0014")]
        MunicipalBond = 14,
        [Description("0015")]
        MBS = 15,
        [Description("0016")]
        NonUnitised = 16,
        [Description("0017")]
        Bespoke = 17
    }

    public enum PriceType
    {
        Closed = 1,
        IntraDay = 2
    }

}
