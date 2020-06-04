namespace serial{

    public class SerialCommands{
        public enum Commands{
            AddIdentity,
            RemoveIdentity,
            EncryptToIdentity,
            DecryptFromIdentity,
            UnknownCommand,
            Exit
        }
    }

}