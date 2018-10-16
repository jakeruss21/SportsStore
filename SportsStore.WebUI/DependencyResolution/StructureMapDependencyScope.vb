
' --------------------------------------------------------------------------------------------------------------------
' <copyright file="StructureMapDependencyScope.vb" company="none">
' Licensed under the Apache License, Version 2.0 (the "License");
' you may not use this file except in compliance with the License.
' You may obtain a copy of the License at
'
' http://www.apache.org/licenses/LICENSE-2.0

' Unless required by applicable law or agreed to in writing, software
' distributed under the License is distributed on an "AS IS" BASIS,
' WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
' See the License for the specific language governing permissions and
' limitations under the License.
' </copyright>
' --------------------------------------------------------------------------------------------------------------------





Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.Http.Dependencies
Imports Microsoft.Practices.ServiceLocation
Imports StructureMap

Namespace DependencyResolution
    ''' <summary>
    ''' The structure map dependency scope.
    ''' </summary>
    Public Class StructureMapDependencyScope
        Inherits ServiceLocatorImplBase
        Implements IDependencyScope
#Region "Constants and Fields"

        ''' <summary>
        ''' The container.
        ''' </summary>
        Protected ReadOnly Container As IContainer

#End Region

#Region "Constructors and Destructors"
        Public StructureMapDependencyScope(IContainer container)
        {
            #If (conatainer == null)
            {
                Throw New ArgumentNullException("container");
            }

        }
        ''' <summary>
        ''' Initializes a new instance of the <see cref="StructureMapDependencyScope"/> class.
        ''' </summary>
        ''' <param name="container">
        ''' The container.
        ''' </param>
        ''' <exception cref="ArgumentNullException">
        ''' </exception>
        Public Sub New(container As IContainer)
            If container Is Nothing Then
                Throw New ArgumentNullException("container")
            End If

            Me.Container = container
        End Sub

#End Region

#Region "Public Methods and Operators"

        ''' <summary>
        ''' The dispose.
        ''' </summary>
        Public Sub Dispose() Implements IDependencyScope.Dispose
            Me.Container.Dispose()
        End Sub

        ''' <summary>
        ''' The get services.
        ''' </summary>
        ''' <param name="serviceType">
        ''' The service type.
        ''' </param>
        ''' <returns>
        ''' The System.Collections.Generic.IEnumerable`1[T -&gt; System.Object].
        ''' </returns>
        Public Function GetServices(serviceType As Type) As IEnumerable(Of Object) Implements IDependencyScope.GetServices
            Return Me.Container.GetAllInstances(serviceType).Cast(Of Object)()
        End Function

        Public Overrides Function GetService(serviceType As Type) As Object Implements IDependencyScope.GetService
            Return MyBase.GetInstance(serviceType)

        End Function

#End Region

#Region "Methods"

        ''' <summary>
        ''' When implemented by inheriting classes, this method will do the actual work of
        '''        resolving all the requested service instances.
        ''' </summary>
        ''' <param name="serviceType">
        ''' Type of service requested.
        ''' </param>
        ''' <returns>
        ''' Sequence of service instance objects.
        ''' </returns>
        Protected Overrides Function DoGetAllInstances(serviceType As Type) As IEnumerable(Of Object)
            Return Me.Container.GetAllInstances(serviceType).Cast(Of Object)()
        End Function

        ''' <summary>
        ''' When implemented by inheriting classes, this method will do the actual work of resolving
        '''        the requested service instance.
        ''' </summary>
        ''' <param name="serviceType">
        ''' Type of instance requested.
        ''' </param>
        ''' <param name="key">
        ''' Name of registered service you want. May be null.
        ''' </param>
        ''' <returns>
        ''' The requested service instance.
        ''' </returns>
        Protected Overrides Function DoGetInstance(serviceType As Type, key As String) As Object
            If String.IsNullOrEmpty(key) Then
                Return If(serviceType.IsAbstract OrElse serviceType.IsInterface, Me.Container.TryGetInstance(serviceType), Me.Container.GetInstance(serviceType))
            End If

            Return Me.Container.GetInstance(serviceType, key)
        End Function

#End Region
    End Class
End Namespace


