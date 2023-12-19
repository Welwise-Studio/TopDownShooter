using System;

namespace Domain.Wallets
{
    ///<summary>
    /// Represents a wallet interface that provides basic functionality for managing a balance.
    ///</summary>
    public interface IWallet : IDisposable
    {
        ///<summary>
        /// Event that gets triggered when the balance is changed.
        ///</summary>
        public Action<uint> OnValueChanged { get; set; }

        ///<summary>
        /// Event that gets triggered when funds are spent from the wallet.
        ///</summary>
        public Action<uint> OnSpend { get; set; }

        ///<summary>
        /// Event that gets triggered when funds are added to the wallet.
        ///</summary>
        public Action<uint> OnAdd { get; set; }

        ///<summary>
        /// Retrieves the current balance of the wallet.
        ///</summary>
        ///<returns>The current balance of the wallet.</returns>
        public uint Get();

        ///<summary>
        /// Adds funds to the wallet.
        ///</summary>
        ///<param name="amount">The amount to add to the balance.</param>
        public void Add(uint amount);

        ///<summary>
        /// Attempts to spend funds from the wallet.
        ///</summary>
        ///<param name="amount">The amount to spend from the wallet.</param>
        ///<returns>True if the wallet has enough funds and the spending is successful, False otherwise.</returns>
        public bool TrySpend(uint amount);


        ///<summary>
        /// Spends funds from the wallet.
        ///</summary>
        ///<param name="amount">The amount to spend from the wallet.</param>
        public void Spend(uint amount);

        ///<summary>
        /// Checks if the wallet has enough funds.
        ///</summary>
        ///<param name="amount">The amount to check for sufficiency.</param>
        ///<returns>True if the wallet has enough funds, False otherwise.</returns>
        public bool HasEnough(uint amount);
    }
}
