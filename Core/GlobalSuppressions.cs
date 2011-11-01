// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the 
// Error List, point to "Suppress Message(s)", and click 
// "In Project Suppression File".
// You do not need to add suppressions to this file manually.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces", Scope = "type", Target = "AutoConfig.Configuration")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Impl", Scope = "type", Target = "AutoConfig.Abstraction.IConfigurationItemImpl")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "AutoConfig.Resolver.Abstraction")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "AutoConfig.Locator")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "AutoConfig.Handlers")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "AutoConfig.Containers")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "AutoConfig.Interceptors")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "AutoConfig.Abstraction.IInvalidator`1.#InvalidatesOn(System.Linq.Expressions.Expression`1<System.Func`2<!0,System.Object>>)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "AutoConfig.Abstraction.IConfigurationItem`1.#Invalidates(System.Linq.Expressions.Expression`1<System.Func`2<!0,System.Object>>)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "AutoConfig.Abstraction.IConfiguration.#Setup`1(System.Linq.Expressions.Expression`1<System.Func`2<!!0,System.Object>>)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "AutoConfig.Abstraction.IConfiguration.#Setup`1(System.Linq.Expressions.Expression`1<System.Action`1<!!0>>)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "AutoConfig.Configuration.#Setup`1(System.Linq.Expressions.Expression`1<System.Action`1<!!0>>)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "AutoConfig.BackEnd.#Setup`1(System.Linq.Expressions.Expression`1<System.Func`2<!!0,System.Object>>)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "AutoConfig.BackEnd.#Setup`1(System.Linq.Expressions.Expression`1<System.Action`1<!!0>>)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "AutoConfig.Configuration.#Setup`1(System.Linq.Expressions.Expression`1<System.Func`2<!!0,System.Object>>)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "AutoConfig.AutoConfigSection.#CacheProvider")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "AutoConfig.AutoConfigSection.#CacheStore")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "cacheDuration", Scope = "member", Target = "AutoConfig.ConfigurationItem`1.#Caches(System.TimeSpan)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "returnValue", Scope = "member", Target = "AutoConfig.MethodInvocation.#SetReturn(System.Object)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "arguments", Scope = "member", Target = "AutoConfig.MethodMetaData.#ValidateArguments(System.Object[])")]
