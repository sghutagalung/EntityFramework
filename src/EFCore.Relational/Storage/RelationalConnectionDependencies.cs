// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Storage
{
    /// <summary>
    ///     <para>
    ///         Service dependencies parameter class for <see cref="RelationalConnection" />
    ///     </para>
    ///     <para>
    ///         This type is typically used by database providers (and other extensions). It is generally
    ///         not used in application code.
    ///     </para>
    ///     <para>
    ///         Do not construct instances of this class directly from either provider or application code as the
    ///         constructor signature may change as new dependencies are added. Instead, use this type in 
    ///         your constructor so that an instance will be created and injected automatically by the 
    ///         dependency injection container. To create an instance with some dependent services replaced, 
    ///         first resolve the object from the dependency injection container, then replace selected 
    ///         services using the 'With...' methods. Do not call the constructor at any point in this process.
    ///     </para>
    /// </summary>
    public sealed class RelationalConnectionDependencies
    {
        /// <summary>
        ///     <para>
        ///         Creates the service dependencies parameter object for a <see cref="RelationalConnection" />.
        ///     </para>
        ///     <para>
        ///         Do not call this constructor directly from either provider or application code as it may change 
        ///         as new dependencies are added. Instead, use this type in your constructor so that an instance 
        ///         will be created and injected automatically by the dependency injection container. To create 
        ///         an instance with some dependent services replaced, first resolve the object from the dependency 
        ///         injection container, then replace selected services using the 'With...' methods. Do not call 
        ///         the constructor at any point in this process.
        ///     </para>
        /// </summary>
        /// <param name="contextOptions"> The options for the current context instance. </param>
        /// <param name="transactionLogger"> The logger to which transaction messages will be written. </param>
        /// <param name="connectionLogger"> The logger to which connection messages will be written. </param>
        /// <param name="diagnosticSource"> The diagnostic source to write to. </param>
        public RelationalConnectionDependencies(
            [NotNull] IDbContextOptions contextOptions,
            [NotNull] IInterceptingLogger<LoggerCategory.Database.Transaction> transactionLogger,
            [NotNull] IInterceptingLogger<LoggerCategory.Database.Connection> connectionLogger,
            [NotNull] DiagnosticSource diagnosticSource)
        {
            Check.NotNull(contextOptions, nameof(contextOptions));
            Check.NotNull(connectionLogger, nameof(connectionLogger));
            Check.NotNull(transactionLogger, nameof(transactionLogger));
            Check.NotNull(diagnosticSource, nameof(diagnosticSource));

            ContextOptions = contextOptions;
            ConnectionLogger = connectionLogger;
            TransactionLogger = transactionLogger;
            DiagnosticSource = diagnosticSource;
        }

        /// <summary>
        ///     The options for the current context instance.
        /// </summary>
        public IDbContextOptions ContextOptions { get; }

        /// <summary>
        ///     The logger to which transaction messages will be written.
        /// </summary>
        public IInterceptingLogger<LoggerCategory.Database.Transaction> TransactionLogger { get; }


        /// <summary>
        ///     The logger to which connection messages will be written.
        /// </summary>
        public IInterceptingLogger<LoggerCategory.Database.Connection> ConnectionLogger { get; }

        /// <summary>
        ///     The diagnostic source to write to.
        /// </summary>
        public DiagnosticSource DiagnosticSource { get; }

        /// <summary>
        ///     Clones this dependency parameter object with one service replaced.
        /// </summary>
        /// <param name="contextOptions">
        ///     A replacement for the current dependency of this type.
        /// </param>
        /// <returns> A new parameter object with the given service replaced. </returns>
        public RelationalConnectionDependencies With([NotNull] IDbContextOptions contextOptions)
            => new RelationalConnectionDependencies(contextOptions, TransactionLogger, ConnectionLogger, DiagnosticSource);

        /// <summary>
        ///     Clones this dependency parameter object with one service replaced.
        /// </summary>
        /// <param name="connectionLogger">
        ///     A replacement for the current dependency of this type.
        /// </param>
        /// <returns> A new parameter object with the given service replaced. </returns>
        public RelationalConnectionDependencies With([NotNull] IInterceptingLogger<LoggerCategory.Database.Connection> connectionLogger)
            => new RelationalConnectionDependencies(ContextOptions, TransactionLogger, connectionLogger, DiagnosticSource);

        /// <summary>
        ///     Clones this dependency parameter object with one service replaced.
        /// </summary>
        /// <param name="transactionLogger">
        ///     A replacement for the current dependency of this type.
        /// </param>
        /// <returns> A new parameter object with the given service replaced. </returns>
        public RelationalConnectionDependencies With([NotNull] IInterceptingLogger<LoggerCategory.Database.Transaction> transactionLogger)
            => new RelationalConnectionDependencies(ContextOptions, transactionLogger, ConnectionLogger, DiagnosticSource);

        /// <summary>
        ///     Clones this dependency parameter object with one service replaced.
        /// </summary>
        /// <param name="diagnosticSource">
        ///     A replacement for the current dependency of this type.
        /// </param>
        /// <returns> A new parameter object with the given service replaced. </returns>
        public RelationalConnectionDependencies With([NotNull] DiagnosticSource diagnosticSource)
            => new RelationalConnectionDependencies(ContextOptions, TransactionLogger, ConnectionLogger, diagnosticSource);
    }
}
