using Azure.Identity;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using System;
using System.Threading.Tasks;

namespace resource_management_preview
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var subscriptionId = Input("Subscription");
            var resourceClient = new ResourcesManagementClient(subscriptionId, new DefaultAzureCredential());
            var resourceGroupsClient = resourceClient.ResourceGroups;

            var location = Input("Location", "japanwest");
            var resourceGroupName = Input("Resource group");
            var resourceGroup = new ResourceGroup(location);
            resourceGroup = await resourceGroupsClient.CreateOrUpdateAsync(resourceGroupName, resourceGroup);

            var response = resourceGroupsClient.ListAsync();
            await foreach (var rg in response)
            {
                Console.WriteLine(rg.Name);
            }
        }

        static string Input(string name, string defaultValue = null)
        {
            if (defaultValue is null)
                Console.Write($"{name}: ");
            else
                Console.Write($"{name} ({defaultValue}): ");

            var value = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(value))
                if (defaultValue is null)
                    return Input(name);
                else
                    return defaultValue;
            return value;
        }
    }
}
