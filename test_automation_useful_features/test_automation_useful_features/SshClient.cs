using System;
using System.Collections.Generic;
using System.IO;
using Renci.SshNet;
using test_automation_useful_features.Helpers;

namespace test_automation_useful_features
{
    public class SshCLientSample
    {
        private string server_ip;
        private string user_name;
        private string user_password;

        ConnectionInfo ConnInfo = null;

        public SshCLientSample(string srv_ip, string uname, string upassword)
        {
            server_ip = srv_ip;
            user_name = uname;
            user_password = upassword;

            PasswordAuthenticationMethod passwordAuthMethod = new PasswordAuthenticationMethod(user_name, user_password);
            ConnInfo = new ConnectionInfo(server_ip, 22, user_name, passwordAuthMethod);
        }

	///
	/// This is used to execute a command on host. The execution result will be returned.
	///    - return false If the command can't be executed because of any reason
	///    - return true if the command is executed
	///
	///    Note: Need to check result returned to see whether the execution succeed
	/// 
        public bool executeSimpleCommand(string simpleCommand, ref string result)
        {
            try
            {

                // Execute a (SHELL) Command - prepare upload directory
                using (var sshclient = new SshClient(ConnInfo))
                {
                    sshclient.Connect();
                    if (sshclient.IsConnected == false)
                        return false;
                    using (var cmd = sshclient.CreateCommand(simpleCommand))
                    {
                        result = cmd.Execute();
                        //Console.WriteLine("Return Value = {0}", cmd.ExitStatus);
                        //Console.WriteLine("Return Result = {0}", result);
                    }
                    sshclient.Disconnect();
                }
            }
            catch (Exception ex)
            {
                LogUtil.log.Error(ex.Message.ToString());
                return false;
            }

            return true;

        }

	///
	/// This is used to execute multiple commands as a batch on host. The execution result will be returned.
	///    - return false If the commands can't be executed because of any reason
	///    - return true if the commands are executed
	///
	///    Note: Need to check result returned to see whether the execution succeed
	/// 
        public bool executeCommands(List<string> commands, ref string result)
        {
            try
            {

                // Execute a (SHELL) Command - prepare upload directory
                using (var sshclient = new SshClient(ConnInfo))
                {
                    sshclient.Connect();
                    if (sshclient.IsConnected == false)
                        return false;
                    result = "";
                    foreach (string commandStr in commands)
                    {
                        using (var cmd = sshclient.CreateCommand(commandStr))
                        {
                            result += cmd.Execute();
                            //Console.WriteLine("Return Value = {0}", cmd.ExitStatus);
                            //Console.WriteLine("Return Result = {0}", result);
                        }
                    }
                    sshclient.Disconnect();
                }
            }
            catch (Exception ex)
            {
                LogUtil.log.Error(ex.Message.ToString());
                return false;
            }

            return true;

        }

        public bool uploadFile(string[] filenames, string dest)
        {
            try
            {
                using (var sftp = new SftpClient(ConnInfo))
                {
                    sftp.Connect();
                    if (sftp.IsConnected == false)
                        return false;
                    foreach (string filename in filenames)
                    {
                        FileInfo f = new FileInfo(filename);
                        using (var uplfileStream = System.IO.File.OpenRead(filename))
                        {
                            sftp.UploadFile(uplfileStream, dest + @"/" + f.Name, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.log.Error(ex.Message.ToString());
                return false;
            }

            return true;
        }
    }
}
