namespace CDR.Data.Queries.Interfaces;
public interface IGetCDRByReferenceQuery
{
    Task<Entities.CDR?> Execute(string cdrReference);
}
