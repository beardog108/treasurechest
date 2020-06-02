using System.Collections.Generic;
using System.Linq;
using System;
using Sodium;
using chestcrypto.exceptions;

namespace chestcrypto{

    namespace session{

        public class Session{

            // Create List of tuples(time, byte[])
            // Where the tuple contains a time stamp for expiry and a ed25519 key
            private List<(long, byte[])> ourPrivateKeys;
            private List<(long, byte[])> theirPublicKeys;

            private byte[] ourMasterPrivateKey;
            private byte[] theirMasterPublicKey;
            private bool strictMode;

            private long messageDelay = 25;

            private int minimumKeyExpireSeconds = 600;

            private void validateKeyLength(byte[] key){
                if (key.Length != 32){
                    throw new InvalidKeyLength();
                }
            }

            private long getEpoch(){
                return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            }

            private void validateTimestamp(long ts){
                if (ts < getEpoch() + minimumKeyExpireSeconds){
                    throw new ArgumentOutOfRangeException();
                }
            }

            private bool publicKeyExists(byte[] key){
                foreach((int, byte[]) k in theirPublicKeys){
                    if (Enumerable.SequenceEqual(k.Item2, key)){
                        return true;
                    }
                }
                return false;
            }

            private bool privateKeyExists(byte[] key){
                foreach((int, byte[]) k in ourPrivateKeys){
                    if (Enumerable.SequenceEqual(k.Item2, key)){
                        return true;
                    }
                }
                return false;
            }

            public Session(byte[] masterPrivate, byte[] masterPublic, bool strictMode, long messageDelay){
                validateKeyLength(masterPrivate);
                validateKeyLength(masterPublic);
                ourMasterPrivateKey = masterPrivate;
                theirMasterPublicKey = masterPublic;
                this.strictMode = strictMode;
                this.messageDelay = messageDelay;
                ourPrivateKeys = new List<(long, byte[])>();
                theirPublicKeys = new List<(long, byte[])>();

            }

            public byte[] getOurMasterPrivate(){return ourMasterPrivateKey;}
            public byte[] getTheirMasterPublic(){return theirMasterPublicKey;}

            public void setMinimumKeyExpireSeconds(int newSeconds){minimumKeyExpireSeconds = newSeconds;}
            public void setMessageDelay(long newDelay){
                messageDelay = newDelay;
            }

            public void addPublic(byte[] publicKey, long timestamp){
                timestamp -= messageDelay; // Subtract some time from the specified timestamp because we don't want to use it close to expiry
                validateKeyLength(publicKey);
                validateTimestamp(timestamp);
                if (publicKeyExists(publicKey)){throw new DuplicatePublicKey();}
                theirPublicKeys.Add((timestamp, publicKey));
            }
            public byte[] getLatestPublicKey(){
                if (theirPublicKeys.Count == 0 && strictMode)
                    throw new NoSessionKeyAvailable();
                var key = theirPublicKeys[theirPublicKeys.Count - 1];
                validateTimestamp(key.Item1);
                return key.Item2;
            }
            public byte[] getLatestPrivateKey(){
                if (ourPrivateKeys.Count == 0 && strictMode)
                    throw new NoSessionKeyAvailable();
                var key = ourPrivateKeys[ourPrivateKeys.Count - 1];
                validateTimestamp(key.Item1);
                return key.Item2;
            }

            public (long, byte[])[] getAllPrivateKeys(){return ourPrivateKeys.ToArray();}

            public void addPrivate(byte[] privateKey, long timestamp){
                validateKeyLength(privateKey);
                validateTimestamp(timestamp);
                if (privateKeyExists(privateKey)){throw new DuplicatePrivateKey();}
                ourPrivateKeys.Add((timestamp, privateKey));
            }

            public void generatePrivate(int secsToExpire = 1200){
                long ts = (long) secsToExpire + getEpoch();
                addPrivate(PublicKeyBox.GenerateKeyPair().PrivateKey, ts);
            }

            public void cleanPublic(){
                long epoch = getEpoch();
                bool expired((long, byte[]) k){
                    if (k.Item1 > epoch){
                        return true;
                    }
                    return false;
                }

                theirPublicKeys.RemoveAll(expired); // remove all keys who are truthy with expired()
            }

            public void cleanPrivate(){
                // Can't use predicate approach because we want to zero out private keys
                if (ourPrivateKeys.Count == 0){
                    return;
                }
                List<int> remove = new List<int>();

                for (int i = 0; i < ourPrivateKeys.Count; i++){
                    if (ourPrivateKeys[i].Item1 > getEpoch()){
                        remove.Add(i);
                        // We manually clear memory to reduce attack surface a tiny bit (GC may take too long)
                        Array.Clear(ourPrivateKeys[i].Item2, 0, ourPrivateKeys[i].Item2.Length);
                    }
                }
                foreach(int i in remove){
                    try{
                        ourPrivateKeys.RemoveAt((int) i);
                    }
                    catch(System.ArgumentOutOfRangeException){
                        ourPrivateKeys.Clear();
                    }
                }
            }

        }

    }

}