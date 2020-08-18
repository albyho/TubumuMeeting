﻿using System;
using System.Collections.Generic;
using System.Threading;
using Tubumu.Core.Extensions.Object;

namespace TubumuMeeting.Mediasoup
{
    public class MediasoupServer
    {
        private readonly List<Worker> _workers = new List<Worker>();

        private int _nextMediasoupWorkerIndex = 0;

        private readonly ReaderWriterLockSlim _workersLocker = new ReaderWriterLockSlim();

        /// <summary>
        /// Get a cloned copy of the mediasoup supported RTP capabilities.
        /// </summary>
        /// <returns></returns>
        public static RtpCapabilities GetSupportedRtpCapabilities()
        {
            return RtpCapabilities.SupportedRtpCapabilities.DeepClone<RtpCapabilities>();
        }

        /// <summary>
        /// Get next mediasoup Worker.
        /// </summary>
        /// <returns></returns>
        public Worker GetWorker()
        {
            _workersLocker.EnterReadLock();
            if (_nextMediasoupWorkerIndex > _workers.Count - 1)
            {
                throw new Exception("none worker");
            }

            var worker = _workers[_nextMediasoupWorkerIndex];
            if (++_nextMediasoupWorkerIndex == _workers.Count)
            {
                _nextMediasoupWorkerIndex = 0;
            }

            _workersLocker.ExitReadLock();
            return worker;
        }

        /// <summary>
        /// Add worker.
        /// </summary>
        /// <param name="worker"></param>
        public void AddWorker(Worker worker)
        {
            if (worker == null)
            {
                throw new ArgumentNullException(nameof(worker));
            }

            _workersLocker.EnterWriteLock();
            _workers.Add(worker);
            _workersLocker.ExitWriteLock();
        }
    }
}
