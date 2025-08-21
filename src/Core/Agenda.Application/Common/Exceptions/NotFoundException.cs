namespace Agenda.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string menssage) : base(menssage) { }

    public NotFoundException(string entityName, object key) : base($"A entidade '{entityName}' com a chave ({key}) não foi encontrada.") { }
}