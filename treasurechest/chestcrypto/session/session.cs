using System.Collections.Generic;
using System;
using chestcrypto.exceptions;

namespace chestcrypto{

    namespace session{

        internal class Session{

            // Create List of tuples(time, byte[])
            // Where the tuple contains a time stamp for expiry and a ed25519 key
            private List<(long, byte[])> ourPrivateKeys;
            private List<(long, byte[])> theirPublicKeys;

            private byte[] ourMasterPrivateKey;
            private byte[] theirMasterPublicKey;
            private bool strictMode;
            private const int minimumKeyExpireSeconds = 60;

            public Session(byte[] masterPrivate, byte[] masterPublic, bool strictMode){
                if(masterPrivate.Length != 32 | masterPublic.Length != 32){throw new InvalidKeyLength();}
                ourMasterPrivateKey = masterPrivate;
                theirMasterPublicKey = masterPublic;
                this.strictMode = strictMode;
                ourPrivateKeys = new List<(long, byte[])>();
                theirPublicKeys = new List<(long, byte[])>();

            }

            public void addPublic(byte[] publicKey, long timestamp){
                if (timestamp < DateTimeOffset.UtcNow.ToUnixTimeSeconds() + minimumKeyExpireSeconds){
                    throw new ArgumentOutOfRangeException();
                }
                theirPublicKeys.Add((timestamp, publicKey));
            }
            public byte[] getLatestPublicKey(){return theirPublicKeys[theirPublicKeys.Count - 1].Item2;}


        }

    }

}