// <copyright file="ObservableDictionary.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace Basics.MVVM
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// An ObservableDictionary in order to bind to a Control in a View and to be notified when the Dictionary changes.
    /// <see href="https://stackoverflow.com/questions/29176922/observabledictionary-binding-to-combobox-display-valuemvvm"/>
    /// <see href="http://blogs.microsoft.co.il/shimmy/2010/12/26/observabledictionarylttkey-tvaluegt-c/"/>
    /// </summary>
    /// <typeparam name="TKey">The Type of the Keys.</typeparam>
    /// <typeparam name="TValue">The Type of the Values.</typeparam>
    public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        private const string COUNTSTRING = "Count";
        private const string INDEXERNAME = "Item[]";
        private const string KEYSNAME = "Keys";
        private const string VALUESNAME = "Values";

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}"/> class.
        /// </summary>
        public ObservableDictionary() => this.Dictionary = new Dictionary<TKey, TValue>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="dictionary">The Dictionary to initialize the new ObservableDictionary.</param>
        public ObservableDictionary(IDictionary<TKey, TValue> dictionary)
            => this.Dictionary = new Dictionary<TKey, TValue>(dictionary);

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="comparer">The IEqualityComparer to initialize the new ObservableDictionary.</param>
        public ObservableDictionary(IEqualityComparer<TKey> comparer)
            => this.Dictionary = new Dictionary<TKey, TValue>(comparer);

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="capacity">The Capacity to initialize the new ObservableDictionary.</param>
        public ObservableDictionary(int capacity) => this.Dictionary = new Dictionary<TKey, TValue>(capacity);

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="dictionary">The Dictionary to initialize the new ObservableDictionary.</param>
        /// <param name="comparer">The IEqualityComparer to initialize the new ObservableDictionary.</param>
        public ObservableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
            => this.Dictionary = new Dictionary<TKey, TValue>(dictionary, comparer);

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="capacity">The Capacity to initialize the new ObservableDictionary.</param>
        /// <param name="comparer">The IEqualityComparer to initialize the new ObservableDictionary.</param>
        public ObservableDictionary(int capacity, IEqualityComparer<TKey> comparer)
            => this.Dictionary = new Dictionary<TKey, TValue>(capacity, comparer);

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <inheritdoc/>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <inheritdoc/>
        public ICollection<TKey> Keys => this.Dictionary.Keys;

        /// <inheritdoc/>
        public ICollection<TValue> Values => this.Dictionary.Values;

        /// <inheritdoc/>
        public int Count => this.Dictionary.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => this.Dictionary.IsReadOnly;

        /// <summary>
        /// Gets the Dictionary.
        /// </summary>
        protected IDictionary<TKey, TValue> Dictionary { get; private set; }

        /// <inheritdoc/>
        public TValue this[TKey key]
        {
            get
            {
                return this.Dictionary[key];
            }

            set
            {
                this.Insert(key, value, false);
            }
        }

        /// <inheritdoc/>
        public void Add(TKey key, TValue value)
        {
            this.Insert(key, value, true);
        }

        /// <inheritdoc/>
        public bool ContainsKey(TKey key)
        {
            return this.Dictionary.ContainsKey(key);
        }

        /// <inheritdoc/>
        public bool Remove(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            this.Dictionary.TryGetValue(key, out TValue value);

            bool removed = this.Dictionary.Remove(key);

            if (removed)
            {
                this.OnCollectionChanged();
            }

            return removed;
        }

        /// <inheritdoc/>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return this.Dictionary.TryGetValue(key, out value);
        }

        /// <inheritdoc/>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.Insert(item.Key, item.Value, true);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            if (this.Dictionary.Count > 0)
            {
                this.Dictionary.Clear();
                this.OnCollectionChanged();
            }
        }

        /// <inheritdoc/>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return this.Dictionary.Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            this.Dictionary.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return this.Remove(item.Key);
        }

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.Dictionary.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.Dictionary).GetEnumerator();
        }

        /// <summary>
        /// Adds a Range of Items to the Dictionary.
        /// </summary>
        /// <param name="items">The Items to Add to the Dictionary.</param>
        public void AddRange(IDictionary<TKey, TValue> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (items.Count > 0)
            {
                if (this.Dictionary.Count > 0)
                {
                    if (items.Keys.Any(k => this.Dictionary.ContainsKey(k)))
                    {
                        throw new ArgumentException("An item with the same key has already been added.");
                    }
                    else
                    {
                        foreach (var item in items)
                        {
                            this.Dictionary.Add(item);
                        }
                    }
                }
                else
                {
                    this.Dictionary = new Dictionary<TKey, TValue>(items);
                }

                this.OnCollectionChanged(NotifyCollectionChangedAction.Add, items.ToArray());
            }
        }

        /// <summary>
        /// Overwritable Method to call when the PropertyChanged Event is raised.
        /// </summary>
        /// <param name="propertyName">The name of the Changed Property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Insert(TKey key, TValue value, bool add)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (this.Dictionary.TryGetValue(key, out TValue item))
            {
                if (add)
                {
                    throw new ArgumentException("An item with the same key has already been added.");
                }

                if (Equals(item, value))
                {
                    return;
                }

                this.Dictionary[key] = value;

                this.OnCollectionChanged(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value));
            }
            else
            {
                this.Dictionary[key] = value;

                this.OnCollectionChanged(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value));
            }
        }

        private void OnCollectionChanged()
        {
            this.OnPropertyChanged();

            this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, KeyValuePair<TKey, TValue> changedItem)
        {
            this.OnPropertyChanged();

            this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, changedItem));
        }

        private void OnCollectionChanged(
            NotifyCollectionChangedAction action,
            KeyValuePair<TKey, TValue> newItem,
            KeyValuePair<TKey, TValue> oldItem)
        {
            this.OnPropertyChanged();

            this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, newItem, oldItem));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, IList newItems)
        {
            this.OnPropertyChanged();

            this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, newItems));
        }

        private void OnPropertyChanged()
        {
            this.OnPropertyChanged(COUNTSTRING);
            this.OnPropertyChanged(INDEXERNAME);
            this.OnPropertyChanged(KEYSNAME);
            this.OnPropertyChanged(VALUESNAME);
        }
    }
}
