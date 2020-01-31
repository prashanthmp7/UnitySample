using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UnitySample.Common.Entities;
using UnitySample.Common.Interfaces;
using static UnitySample.Common.Consts.Consts;

namespace UnitySample
{
    public class MainWindowViewModel 
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ITestServiceRequests _testServiceRequestFactory;
        private ICommand _getDataCommand;
        public MainWindowViewModel(
            IEventAggregator eventAggregator,
            Func<OnlineMode, ITestServiceRequests> testServiceRequestFactory)
        {
            _eventAggregator = eventAggregator;
            _testServiceRequestFactory = testServiceRequestFactory(OnlineMode.Online);
        }

        /// <summary>
        /// Gets the pin command.
        /// </summary>
        /// <value>
        /// The pin command.
        /// </value>
        public ICommand GetDataCommand
        {
            get
            {
                if (_getDataCommand == null)
                    _getDataCommand = new DelegateCommand( async() =>
                    {
                        await getData();
                    });

                return _getDataCommand;
            }
        }

        private async Task getData()
        {

            var listOfObjects = await _testServiceRequestFactory.GetListOfRandomObjects();
            RandomObjects = new ObservableCollection<RandomObject>();
            RandomObjects.AddRange(listOfObjects);

            var result = await _testServiceRequestFactory.FindRandomStringObjectAsync(2);
        }

        private ObservableCollection<RandomObject> randomObjects;

        public virtual ObservableCollection<RandomObject> RandomObjects
        {
            get { return randomObjects; }
            set { randomObjects = value; }
        }

    }
}
