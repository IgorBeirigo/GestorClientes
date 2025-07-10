using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GestorClientes
{
    class Program
    {
        [System.Serializable]
        struct Cliente
        {
            public string Nome;
            public string Email;
            public string cpf;
        }
            static List <Cliente> clientes = new List<Cliente>();
        private static int i;

        enum Menu { Listagem = 1, Adicionar = 2, Remover = 3, Sair = 4 }
        static void Main(string[] args)
        {
            Carregar();
            bool escolheuSair = false;
            while (!escolheuSair)
            {
                Console.WriteLine("Sistema de Clientes");
                Console.WriteLine("1-listagem\n2-adicionar\n3-remover\n4-sair");
                int intOp = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOp;
                switch (opcao)
                {
                    case Menu.Listagem:
                        Console.WriteLine("Listagem de clientes");
                        Listagem();
                        // Aqui você pode adicionar a lógica para listar os clientes
                        break;
                    case Menu.Adicionar:
                        Console.WriteLine("Adicionar cliente");
                        Adicionar();
                        // Aqui você pode adicionar a lógica para adicionar um cliente
                        break;
                    case Menu.Remover:
                        Console.WriteLine("Remover cliente");
                        Remover();
                        // Aqui você pode adicionar a lógica para remover um cliente
                        break;
                    case Menu.Sair:
                        escolheuSair = true;
                        
                        Console.WriteLine("Saindo do sistema...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
                Console.Clear();
                    }

            }
        static void Adicionar()
        {  Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de Cliente");
            Console.WriteLine("Digite o nome do cliente:");
            cliente.Nome = Console.ReadLine();
            Console.WriteLine("Digite o email do cliente:");
            cliente.Email = Console.ReadLine();
            Console.WriteLine("Digite o CPF do cliente:");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente);
            Salvar();

            Console.WriteLine("Cliente adicionado com sucesso!");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            
        }
        static void Listagem()
        {

            if (clientes.Count == 0)
            {
                Console.WriteLine("Nenhum cliente cadastrado.");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Listagem de Clientes:");
            int i = 0;
            foreach(Cliente cliente in clientes)
                
            {
                Console.WriteLine($"ID:{i}");
                Console.WriteLine($"Nome: {cliente.Nome}, Email: {cliente.Email}, CPF: {cliente.cpf}");
                Console.WriteLine("-----------------------------");
                i++;
                

            }
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadLine();
        }
        static void Remover()
        {
            Listagem();
            Console.WriteLine("Digite o ID do cliente que deseja remover:");
            int id = int.Parse(Console.ReadLine());
            if (id < 0 || id >= clientes.Count)
            {
                Console.WriteLine("ID inválido.");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadLine();
                return;
            }
            clientes.RemoveAt(id);
            Salvar();
            Console.WriteLine("Cliente removido com sucesso!");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadLine();
        }
        static void Salvar()
        {
            FileStream stream = new FileStream("clientes.dat", FileMode.OpenOrCreate);

            BinaryFormatter encoder = new BinaryFormatter();
            encoder.Serialize(stream, clientes);

            stream.Close();
        }
        static void Carregar()
        {
            try
            {
                FileStream stream = new FileStream("clientes.dat", FileMode.OpenOrCreate);

                BinaryFormatter encoder = new BinaryFormatter();
                clientes = (List<Cliente>)encoder.Deserialize(stream);

                if(clientes == null)
                {
                    clientes = new List<Cliente>();
                }

                stream.Close();

            } catch(Exception e)
            {
                clientes = new List<Cliente>();
            }
          

        }
    }
}
