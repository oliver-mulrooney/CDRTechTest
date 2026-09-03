namespace CDR.Data.Commands.Interfaces;
public interface IAddCDRCommand
{
    Task<List<Entities.CDR>> Execute(List<Entities.CDR> cdrs);
}
