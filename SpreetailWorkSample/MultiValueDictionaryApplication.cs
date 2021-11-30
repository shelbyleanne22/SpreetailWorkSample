using Microsoft.Extensions.Logging;
using SpreetailWorkSample.Constants;
using SpreetailWorkSample.Interfaces;
using SpreetailWorkSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreetailWorkSample
{
    public class MultiValueDictionaryApplication
    {
        private readonly ILogger _logger;
        private readonly IPrintService _printService;
        private readonly IMultiValueDictionaryService _multiValueDictionaryService;

        public MultiValueDictionaryApplication(ILogger<MultiValueDictionaryApplication> logger,
            IMultiValueDictionaryService multiValueDictionaryService,
            IPrintService printService)
        {
            _logger = logger;
            _multiValueDictionaryService = multiValueDictionaryService;
            _printService = printService;
        }

        public void Start()
        {
            _logger.LogInformation($"Multi-Value Dictionary application started. Enter {CommandConstants.QuitCommand} to exit.");

            bool quit = false;
            while (!quit)
            {
                string command = Console.ReadLine();

                if (string.IsNullOrEmpty(command))
                {
                    _logger.LogError("Please, enter a command.");
                    break;
                }
                string[] commandArguments = command.Split();

                if (commandArguments[0] == CommandConstants.KeysCommand && commandArguments.Length == 1)
                {
                    var results = _multiValueDictionaryService.GetAllKeys();
                    if (results.Count != 0)
                    {
                        _printService.Print(results);
                    }
                    else
                    {
                        _printService.Print(MessageConstants.EmptySetMessage);
                    }

                }
                else if (commandArguments[0] == CommandConstants.MembersCommand && commandArguments.Length == 2)
                {
                    bool keyExists = _multiValueDictionaryService.KeyExists(commandArguments[1]);
                    if (keyExists)
                    {
                        var results = _multiValueDictionaryService.GetAllMembersOfKey(commandArguments[1]);
                        _printService.Print(results);
                    }
                    else
                    {
                        _printService.Print(MessageConstants.KeyDoesNotExistMessage);
                    }                   

                }
                else if (commandArguments[0] == CommandConstants.AddCommand && commandArguments.Length == 3)
                {
                    bool memberExists = _multiValueDictionaryService.MemberExists(commandArguments[1], commandArguments[2]);
                    if (!memberExists)
                    {
                        _multiValueDictionaryService.AddMember(commandArguments[1], commandArguments[2]);
                        _printService.Print(MessageConstants.AddedMessage);
                    } else
                    {
                        _printService.Print(MessageConstants.MemberAlreadyExistsMessage);
                    }
                    
                }
                else if (commandArguments[0] == CommandConstants.RemoveCommand && commandArguments.Length == 3)
                {
                    bool keyExists = _multiValueDictionaryService.KeyExists(commandArguments[1]);
                    bool memberExists = _multiValueDictionaryService.MemberExists(commandArguments[1], commandArguments[2]);

                    if(keyExists && memberExists)
                    {
                        _multiValueDictionaryService.RemoveMember(commandArguments[1], commandArguments[2]);
                        _printService.Print(MessageConstants.RemovedMessage);
                    } else if(!keyExists)
                    {
                        _printService.Print(MessageConstants.KeyDoesNotExistMessage);
                    } else
                    {
                        _printService.Print(MessageConstants.MemberDoesNotExistMessage);
                    }
                    
                }
                else if (commandArguments[0] == CommandConstants.RemoveAllCommand && commandArguments.Length == 2)
                {
                    bool keyExists = _multiValueDictionaryService.KeyExists(commandArguments[1]);
                    if (keyExists)
                    {
                        _multiValueDictionaryService.RemoveAllMembers(commandArguments[1]);
                        _printService.Print(MessageConstants.RemovedMessage);
                    } else
                    {
                        _printService.Print(MessageConstants.KeyDoesNotExistMessage);
                    }
                    
                }
                else if (commandArguments[0] == CommandConstants.ClearCommand && commandArguments.Length == 1)
                {
                    _multiValueDictionaryService.Clear();
                    _printService.Print(MessageConstants.ClearedMessage);
                }
                else if (commandArguments[0] == CommandConstants.KeyExistsCommand && commandArguments.Length == 2)
                {
                    _printService.Print(_multiValueDictionaryService.KeyExists(commandArguments[1]).ToString());
                }
                else if (commandArguments[0] == CommandConstants.MemberExistsCommand && commandArguments.Length == 3)
                {
                    _printService.Print(_multiValueDictionaryService.MemberExists(commandArguments[1], commandArguments[2]).ToString());
                }
                else if (commandArguments[0] == CommandConstants.AllMembersCommand && commandArguments.Length == 1)
                {
                    var results = _multiValueDictionaryService.GetAllMembers();
                    if (results.Count != 0)
                    {
                        _printService.Print(results);
                    } else
                    {
                        _printService.Print(MessageConstants.EmptySetMessage);
                    }
                }
                else if (commandArguments[0] == CommandConstants.ItemsCommand && commandArguments.Length == 1)
                {
                    var results = _multiValueDictionaryService.GetAllItems();
                    if(results.Count != 0)
                    {
                        _printService.Print(results);
                    } else
                    {
                        _printService.Print(MessageConstants.EmptySetMessage);
                    }
                    
                }
                else if(commandArguments[0] == CommandConstants.CountKeysCommand && commandArguments.Length == 1)
                {
                    _printService.Print(_multiValueDictionaryService.CountKeys().ToString());
                }
                else if(commandArguments[0] == CommandConstants.CountMembersCommand && commandArguments.Length == 2)
                {
                    bool keyExists = _multiValueDictionaryService.KeyExists(commandArguments[1]);
                    if (keyExists)
                    {
                        _printService.Print(_multiValueDictionaryService.CountMembers(commandArguments[1]).ToString());
                    } else
                    {
                        _printService.Print(MessageConstants.KeyDoesNotExistMessage);
                    }
                    
                }
                else if (commandArguments[0] == CommandConstants.QuitCommand && commandArguments.Length == 1)
                {
                    quit = true;
                }
                else
                {
                    _logger.LogError("Invalid command, please try again.");
                }
            }

            Stop();
        }

        public void Stop()
        {
            _logger.LogInformation("Multi-Value Dictionary application has stopped.");
        }

        public void HandleError(Exception ex)
        {
            _logger.LogError($"Multi-Value Dictionary application encountered error: {ex.Message}");
        }
    }

}
