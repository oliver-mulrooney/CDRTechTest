using CDR.Data.Commands.Interfaces;
using CDR.Model.Models.CSV;
using CDR.Model.Responses;
using CDR.Service.Mappers.Interfaces;
using CDR.Service.Services;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using Xunit;

namespace CDR.Service.Tests.Services;
public class CDRServiceTests : TestBase<CDRService>
{
    [Fact]
    public async Task CreateCdrsFromCsv_GeneratesCdrsFromCsv()
    {
        //Arrange
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);

        var stubCallerId = "4412312321";
        var stubRecipient = "4431232132";
        var stubCallDate = "10/10/2026";
        var stubEndTime = "14:00:47";
        int stubDuration = 15;
        double stubCost = 12;
        var stubReference = "testing";
        var stubCurrency = Data.Enums.CurrencyEnum.GBP;

        writer.WriteLine("caller_id,recipient,call_date,end_time,duration,cost,reference,currency");
        writer.WriteLine($"{stubCallerId},{stubRecipient},{stubCallDate},{stubEndTime},{stubDuration},{stubCost},{stubReference},{stubCurrency}");
        writer.Flush();
        stream.Position = 0;
        var stubFormFile = new FormFile(stream, 0, stream.Length, "test", "test");

        var stubCdrEntities = new List<Data.Entities.CDR>()
        {
            new Data.Entities.CDR()
            {
                CallerId = stubCallerId,
                Recipient = stubRecipient,
                CallDate = DateTime.Parse(stubCallDate),
                CallType = Data.Enums.CallTypeEnum.Domestic,
                Duration = stubDuration,
                Cost = stubCost,
                Currency = Data.Enums.CurrencyEnum.GBP,
                EndTime = TimeSpan.Parse(stubEndTime),
                Reference = stubReference,
            }
        };

        this.autoMocker.GetMock<ICDRCsvRecordsToEntitiesMapper>()
            .Setup(x => x.Map(It.IsAny<List<CDRCsvRecord>>()))
            .Returns(stubCdrEntities);

        this.autoMocker.GetMock<IAddCDRCommand>()
            .Setup(x => x.Execute(stubCdrEntities))
            .ReturnsAsync(stubCdrEntities);

        var stubCDRUploadSummary = new CDRUploadSummaryResponse()
        {
            IsSuccessful = true,
            TotalRecordsUploaded = stubCdrEntities.Count,
            ErrorMessage = null
        };

        this.autoMocker.GetMock<ICDRUploadSummaryMapper>()
            .Setup(x => x.Map(stubCdrEntities, true, null))
            .Returns(stubCDRUploadSummary);

        //Act
        var sut = CreateSubjectUnderTest();

        var result = await sut.CreateCdrsFromCsv(stubFormFile);

        //Assert
        Assert.Equal(1, result.TotalRecordsUploaded);
        Assert.True(result.IsSuccessful);
        Assert.Null(result.ErrorMessage);

        this.autoMocker.GetMock<ICDRCsvRecordsToEntitiesMapper>()
            .Verify(x => x.Map(It.IsAny<List<CDRCsvRecord>>()), Times.Once);

        this.autoMocker.GetMock<IAddCDRCommand>()
            .Verify(x => x.Execute(stubCdrEntities), Times.Once);

        this.autoMocker.GetMock<ICDRUploadSummaryMapper>()
            .Verify(x => x.Map(stubCdrEntities, true, null), Times.Once);
    }

    [Fact]
    public async Task CreateCdrsFromCsv_HandlesException()
    {
        //Arrange
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);

        var stubCallerId = "4412312321";
        var stubRecipient = "4431232132";
        var stubCallDate = "10/10/2026";
        var stubEndTime = "14:00:47";
        int stubDuration = 15;
        double stubCost = 12;
        var stubReference = "testing";
        var stubCurrency = Data.Enums.CurrencyEnum.GBP;

        writer.WriteLine("caller_id,recipient,call_date,end_time,duration,cost,reference,currency");
        writer.WriteLine($"{stubCallerId},{stubRecipient},{stubCallDate},{stubEndTime},{stubDuration},{stubCost},{stubReference},{stubCurrency}");
        writer.Flush();
        stream.Position = 0;
        var stubFormFile = new FormFile(stream, 0, stream.Length, "test", "test");

        var stubCdrEntities = new List<Data.Entities.CDR>()
        {
            new Data.Entities.CDR()
            {
                CallerId = stubCallerId,
                Recipient = stubRecipient,
                CallDate = DateTime.Parse(stubCallDate),
                CallType = Data.Enums.CallTypeEnum.Domestic,
                Duration = stubDuration,
                Cost = stubCost,
                Currency = Data.Enums.CurrencyEnum.GBP,
                EndTime = TimeSpan.Parse(stubEndTime),
                Reference = stubReference,
            }
        };

        var stubErrorMessage = "Error";

        this.autoMocker.GetMock<ICDRCsvRecordsToEntitiesMapper>()
            .Setup(x => x.Map(It.IsAny<List<CDRCsvRecord>>()))
            .Throws(new Exception(stubErrorMessage));

        var stubCDRUploadSummary = new CDRUploadSummaryResponse()
        {
            IsSuccessful = false,
            TotalRecordsUploaded = 0,
            ErrorMessage = stubErrorMessage
        };

        this.autoMocker.GetMock<ICDRUploadSummaryMapper>()
            .Setup(x => x.Map(null, false, stubErrorMessage))
            .Returns(stubCDRUploadSummary);

        //Act
        var sut = CreateSubjectUnderTest();

        var result = await sut.CreateCdrsFromCsv(stubFormFile);

        //Assert
        Assert.Equal(0, result.TotalRecordsUploaded);
        Assert.False(result.IsSuccessful);
        Assert.Equal(stubErrorMessage ,result.ErrorMessage);

        this.autoMocker.GetMock<ICDRCsvRecordsToEntitiesMapper>()
            .Verify(x => x.Map(It.IsAny<List<CDRCsvRecord>>()), Times.Once);

        this.autoMocker.GetMock<IAddCDRCommand>()
            .Verify(x => x.Execute(stubCdrEntities), Times.Never);

        this.autoMocker.GetMock<ICDRUploadSummaryMapper>()
            .Verify(x => x.Map(null, false, stubErrorMessage), Times.Once);
    }
}
