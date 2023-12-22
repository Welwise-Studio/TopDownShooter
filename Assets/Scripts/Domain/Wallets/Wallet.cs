using System;

namespace Domain.Wallets
{
    ///<summary>
    /// Represents a wallet that stores and manages a balance.
    ///</summary>
    public class Wallet : IWallet
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

        private uint _balance = 0;

        ///<summary>
        /// Initializes a new instance of the Wallet class with a given balance.
        ///</summary>
        ///<param name="balance">The initial balance of the wallet.</param>
        public Wallet(uint balance)
        {
            _balance = balance;
        }

        ///<summary>
        /// Adds funds to the wallet.
        ///</summary>
        ///<param name="amount">The amount to add to the balance.</param>
        public void Add(uint amount)
        {
            _balance += amount;
            OnAdd?.Invoke(amount);
            OnValueChanged?.Invoke(_balance);
        }

        ///<summary>
        /// Retrieves the current balance of the wallet.
        ///</summary>
        ///<returns>The current balance of the wallet.</returns>
        public uint Get() => _balance;

        ///<summary>
        /// Checks if the wallet has enough funds.
        ///</summary>
        ///<param name="amount">The amount to check for sufficiency.</param>
        ///<returns>True if the wallet has enough funds, False otherwise.</returns>
        public bool HasEnough(uint amount) => _balance >= amount;

        ///<summary>
        /// Spends funds from the wallet.
        ///</summary>
        ///<param name="amount">The amount to spend from the wallet.</param>
        public void Spend(uint amount) => TrySpend(amount);

        ///<summary>
        /// Attempts to spend funds from the wallet.
        ///</summary>
        ///<param name="amount">The amount to spend from the wallet.</param>
        ///<returns>True if the wallet has enough funds and the spending is successful, False otherwise.</returns>
        public bool TrySpend(uint amount)
        {
            if (!HasEnough(amount))
                return false;

            _balance -= amount;
            OnSpend?.Invoke(amount);
            OnValueChanged?.Invoke(_balance);
            return true;
        }

        ///<summary>
        /// Disposes the wallet and clears the event handlers.
        ///</summary>
        public void Dispose()
        {
            OnValueChanged = null;
            OnSpend = null;
            OnAdd = null;
        }
    }
}
