using EverscaleNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EverscaleNet.Abstract.Modules
{
    /// <summary>
    /// Proofs Module
    /// </summary>
    public interface IProofsModule : IEverModule
    {
        /// <summary>
        /// <para>Proves that a given block's data, which is queried from TONOS API, can be trusted.</para>
        /// <para>This function checks block proofs and compares given data with the proven.</para>
        /// <para>If the given data differs from the proven, the exception will be thrown.</para>
        /// <para>The input param is a single block's JSON object, which was queried from DApp server using</para>
        /// <para>functions such as `net.query`, `net.query_collection` or `net.wait_for_collection`.</para>
        /// <para>If block's BOC is not provided in the JSON, it will be queried from DApp server</para>
        /// <para>(in this case it is required to provide at least `id` of block).</para>
        /// <para>Please note, that joins (like `signatures` in `Block`) are separated entities and not supported,</para>
        /// <para>so function will throw an exception in a case if JSON being checked has such entities in it.</para>
        /// <para>If `cache_in_local_storage` in config is set to `true` (default), downloaded proofs and</para>
        /// <para>master-chain BOCs are saved into the persistent local storage (e.g. file system for native</para>
        /// <para>environments or browser's IndexedDB for the web); otherwise all the data is cached only in</para>
        /// <para>memory in current client's context and will be lost after destruction of the client.</para>
        /// <para>**Why Proofs are needed**</para>
        /// <para>Proofs are needed to ensure that the data downloaded from a DApp server is real blockchain</para>
        /// <para>data. Checking proofs can protect from the malicious DApp server which can potentially provide</para>
        /// <para>fake data, or also from "Man in the Middle" attacks class.</para>
        /// <para>**What Proofs are**</para>
        /// <para>Simply, proof is a list of signatures of validators', which have signed this particular master-</para>
        /// <para>block.</para>
        /// <para>The very first validator set's public keys are included in the zero-state. Whe know a root hash</para>
        /// <para>of the zero-state, because it is stored in the network configuration file, it is our authority</para>
        /// <para>root. For proving zero-state it is enough to calculate and compare its root hash.</para>
        /// <para>In each new validator cycle the validator set is changed. The new one is stored in a key-block,</para>
        /// <para>which is signed by the validator set, which we already trust, the next validator set will be</para>
        /// <para>stored to the new key-block and signed by the current validator set, and so on.</para>
        /// <para>In order to prove any block in the master-chain we need to check, that it has been signed by</para>
        /// <para>a trusted validator set. So we need to check all key-blocks' proofs, started from the zero-state</para>
        /// <para>and until the block, which we want to prove. But it can take a lot of time and traffic to</para>
        /// <para>download and prove all key-blocks on a client. For solving this, special trusted blocks are used</para>
        /// <para>in TON-SDK.</para>
        /// <para>The trusted block is the authority root, as well, as the zero-state. Each trusted block is the</para>
        /// <para>`id` (e.g. `root_hash`) of the already proven key-block. There can be plenty of trusted</para>
        /// <para>blocks, so there can be a lot of authority roots. The hashes of trusted blocks for MainNet</para>
        /// <para>and DevNet are hardcoded in SDK in a separated binary file (trusted_key_blocks.bin) and is</para>
        /// <para>being updated for each release by using `update_trusted_blocks` utility.</para>
        /// <para>See [update_trusted_blocks](../../../tools/update_trusted_blocks) directory for more info.</para>
        /// <para>In future SDK releases, one will also be able to provide their hashes of trusted blocks for</para>
        /// <para>other networks, besides for MainNet and DevNet.</para>
        /// <para>By using trusted key-blocks, in order to prove any block, we can prove chain of key-blocks to</para>
        /// <para>the closest previous trusted key-block, not only to the zero-state.</para>
        /// <para>But shard-blocks don't have proofs on DApp server. In this case, in order to prove any shard-</para>
        /// <para>block data, we search for a corresponding master-block, which contains the root hash of this</para>
        /// <para>shard-block, or some shard block which is linked to that block in shard-chain. After proving</para>
        /// <para>this master-block, we traverse through each link and calculate and compare hashes with links,</para>
        /// <para>one-by-one. After that we can ensure that this shard-block has also been proven.</para>
        /// </summary>
        public Task ProofBlockData(ParamsOfProofBlockData @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Proves that a given transaction's data, which is queried from TONOS API, can be trusted.</para>
        /// <para>This function requests the corresponding block, checks block proofs, ensures that given</para>
        /// <para>transaction exists in the proven block and compares given data with the proven.</para>
        /// <para>If the given data differs from the proven, the exception will be thrown.</para>
        /// <para>The input parameter is a single transaction's JSON object (see params description),</para>
        /// <para>which was queried from TONOS API using functions such as `net.query`, `net.query_collection`</para>
        /// <para>or `net.wait_for_collection`.</para>
        /// <para>If transaction's BOC and/or `block_id` are not provided in the JSON, they will be queried from</para>
        /// <para>TONOS API.</para>
        /// <para>Please note, that joins (like `account`, `in_message`, `out_messages`, etc. in `Transaction`</para>
        /// <para>entity) are separated entities and not supported, so function will throw an exception in a case</para>
        /// <para>if JSON being checked has such entities in it.</para>
        /// <para>For more information about proofs checking, see description of `proof_block_data` function.</para>
        /// </summary>
        public Task ProofTransactionData(ParamsOfProofTransactionData @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Proves that a given message's data, which is queried from TONOS API, can be trusted.</para>
        /// <para>This function first proves the corresponding transaction, ensures that the proven transaction</para>
        /// <para>refers to the given message and compares given data with the proven.</para>
        /// <para>If the given data differs from the proven, the exception will be thrown.</para>
        /// <para>The input parameter is a single message's JSON object (see params description),</para>
        /// <para>which was queried from TONOS API using functions such as `net.query`, `net.query_collection`</para>
        /// <para>or `net.wait_for_collection`.</para>
        /// <para>If message's BOC and/or non-null `src_transaction.id` or `dst_transaction.id` are not provided</para>
        /// <para>in the JSON, they will be queried from TONOS API.</para>
        /// <para>Please note, that joins (like `block`, `dst_account`, `dst_transaction`, `src_account`,</para>
        /// <para>`src_transaction`, etc. in `Message` entity) are separated entities and not supported,</para>
        /// <para>so function will throw an exception in a case if JSON being checked has such entities in it.</para>
        /// <para>For more information about proofs checking, see description of `proof_block_data` function.</para>
        /// </summary>
        public Task ProofMessageData(ParamsOfProofMessageData @params, CancellationToken cancellationToken = default);
    }
}