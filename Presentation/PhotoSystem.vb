Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D

Public Class PhotoSystem
    Private Const MAX_PHOTOS = 20

    Private m_Device As Device
    Private m_Photos As List(Of Photo)
    Private m_Home As Vector3

    Public Sub New(ByVal device As Device, ByVal position As Vector3)
        m_Device = device
        m_Home = position

        m_Photos = New List(Of Photo)
    End Sub

    Public Sub AddPhoto(ByVal texture As Texture)
        'Create the new photo and add it to the stack
        m_Photos.Add(New Photo(m_Device, New Vector3(m_Home.X, m_Home.Y, m_Home.Z - (m_Photos.Count * 5)), texture))

        'Scatter the stack
        For Each p As Photo In m_Photos
            p.Scatter()
            'If there are more photos in the stack than we want, 
            'start passing the textures down the stack
            If m_Photos.Count > MAX_PHOTOS Then
	            If m_Photos.Count > m_Photos.IndexOf(p) + 1 Then
	                p.Texture = m_Photos(m_Photos.IndexOf(p) + 1).Texture
	            End If
	    End If
        Next

        'Drop the last photo from the stack (but the stack is reverse ordered so actually drop the first item)
        If m_Photos.Count > MAX_PHOTOS Then
            m_Photos.RemoveAt(MAX_PHOTOS - 1)
        End If
    End Sub

    Public Sub Render()
        For Each p As Photo In m_Photos
            p.Render()
        Next
    End Sub

    Public Sub Update(ByVal deltaTime As Single)
        For Each p As Photo In m_Photos
            p.Update(deltaTime)
        Next
    End Sub
End Class
