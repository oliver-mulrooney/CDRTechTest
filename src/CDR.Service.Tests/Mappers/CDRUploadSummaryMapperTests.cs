using CDR.Service.Mappers;
using Moq;
using Xunit;

namespace CDR.Service.Tests.Mappers;
public class CDRUploadSummaryMapperTests : TestBase<CDRUploadSummaryMapper>
{
    [Fact]
    public void Map_MapsCdrEntitiesToSummary_WhenNoErrorsPresent()
    {
        //Arrange
        var stubCdrEntities = new List<Data.Entities.CDR>()
        {
            new Mock<Data.Entities.CDR>().Object,
            new Mock<Data.Entities.CDR>().Object,
            new Mock<Data.Entities.CDR>().Object,
            new Mock<Data.Entities.CDR>().Object
        };

        //Act
        var sut = CreateSubjectUnderTest();

        var result = sut.Map(stubCdrEntities, true, null);

        //Assert
        Assert.Equal(stubCdrEntities.Count, result.TotalRecordsUploaded);
        Assert.True(result.IsSuccessful); 
        Assert.Null(result.ErrorMessage);
    }

    [Fact]
    public void Map_MapsCdrEntitiesToSummary_WhenErrorThrown()
    {
        //Arrange
        var stubErrorMessage = "It's broken";

        //Act
        var sut = CreateSubjectUnderTest();

        var result = sut.Map(null, false, stubErrorMessage);

        //Assert
        Assert.Equal(0, result.TotalRecordsUploaded);
        Assert.False(result.IsSuccessful);
        Assert.Equal(stubErrorMessage, result.ErrorMessage);
    }
}
