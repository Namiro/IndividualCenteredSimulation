using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Helpers.Services
{
    /// <summary>
    /// This class is the base class for every service class
    /// </summary>
    public class Service : INotifyPropertyChanged
    {
        /// <summary>
        /// This event allow to get the event if a property of any instantiate object change a property.
        /// </summary>
        public static event PropertyChangedEventHandler PropertyChangedStatic;

        /// <summary>
        /// This event allow to get the event if a property of a precise object instanciate change its property.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Allow to raise property changement
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            var handlerr = PropertyChangedStatic;
            if (handlerr != null) handlerr(this, new PropertyChangedEventArgs(propertyName));
        }

        protected static void RaisePropertyStaticChanged([CallerMemberName] string propertyName = null)
        {
            var handlerr = PropertyChangedStatic;
            if (handlerr != null) handlerr(null, new PropertyChangedEventArgs(propertyName));
        }
    }

}