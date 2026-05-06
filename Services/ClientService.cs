using ClientHub.Models;
using System.Collections.Generic;
using System.Linq;

public class ClientService
{
    private static readonly object _lock = new();
    private static List<Client> clients = new();
    private static int id = 1;

    public List<Client> GetAll()
    {
        lock (_lock)
        {
            return clients.ToList();
        }
    }

    public Client GetById(int clientId)
    {
        lock (_lock)
        {
            return clients.FirstOrDefault(x => x.Id == clientId);
        }
    }

    public Client Create(Client c)
    {
        if (c == null) return null;

        lock (_lock)
        {
            c.Id = id++;
            clients.Add(c);
            return c;
        }
    }

    public Client Update(Client c)
    {
        lock (_lock)
        {
            var existing = clients.FirstOrDefault(x => x.Id == c.Id);
            if (existing == null) return null;

            existing.Name = c.Name;
            existing.Email = c.Email;

            return existing;
        }
    }

    public bool Delete(int clientId)
    {
        lock (_lock)
        {
            var client = clients.FirstOrDefault(x => x.Id == clientId);
            if (client == null) return false;

            clients.Remove(client);
            return true;
        }
    }
}