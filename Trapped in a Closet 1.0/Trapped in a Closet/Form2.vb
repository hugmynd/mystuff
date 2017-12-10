Public Class Form2

    'Brian Petursson
    'Final Project 2013

    Dim intUserRoom As Integer      'keeps track of the in-game room the user is in
    Dim intPoints As Integer        'keeps track of the number of points that the user has

    Private Sub Form2_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Form1.Hide()

        Me.rtxtGameLog.Text = "Enter in start to begin" & vbCrLf & vbCrLf & "New players may want to look at [Instructions] " & _
            "under the [Menu] tab."

    End Sub

    Private Sub txtUserAction_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtUserAction.KeyDown

        If e.KeyValue = Keys.Enter Then
            Call btnEnterAction_Click(sender, e)
        End If

    End Sub

    Private Sub btnEnterAction_Click(sender As Object, e As System.EventArgs) Handles btnEnterAction.Click

        Dim strUserWord1 As String = Nothing
        Dim strUserWord2 As String = Nothing
        Dim strUserAction As String = Nothing

        Call StringSeperation(strUserAction, strUserWord1, strUserWord2)

        Call TextInterpret(strUserWord1, strUserWord2)

        Me.txtUserAction.Text = Nothing

    End Sub

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'Seperates text from txtUserAction in to two seperate words
    'that are returned as strings strUserWord1 and strUserWord2
    '
    'post: strUserWord1 and strUserWord2 are sent to TextInterpret
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Sub StringSeperation(ByVal strUserAction As String, ByRef strUserWord1 As String, ByRef strUserWord2 As String)

        strUserAction = Me.txtUserAction.Text

        Dim intTextLength As Integer
        Dim intStart As Integer
        Dim intSpacePosition As Integer

        intSpacePosition = Nothing
        intTextLength = strUserAction.Length

        For intStart = 0 To (intTextLength - 1)
            Dim strTemporaryText As String
            strTemporaryText = strUserAction.Substring(intStart, 1)
            If strTemporaryText = " " Then
                intSpacePosition = intStart
            End If
        Next intStart

        If intSpacePosition <> Nothing Then
            strUserWord2 = strUserAction.Substring(intSpacePosition + 1, intTextLength - intSpacePosition - 1)
            strUserWord2 = strUserWord2.ToLower
            strUserWord1 = strUserAction.Substring(0, intSpacePosition)
        Else
            strUserWord2 = Nothing
            strUserWord1 = strUserAction
        End If

        intSpacePosition = Nothing
        strUserWord1 = strUserWord1.ToLower

    End Sub

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'Checks text for non-room specific interpretations including 'restart', 'help', and 'clear log'.
    'Passes text on to a room-specific interpretations corresponding to intRoom.
    '
    'post: Updates the user log and game progress.
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    Sub TextInterpret(ByVal strUserWord1 As String, ByVal strUserWord2 As String)

        Dim strGameLog As String                    'The game log
        Dim strNewGameLog As String                 'Copy of the game log after interpretation, for comparison with strGameLog
        Static blnSpiderFeed As Boolean = True      'Turns false if the user has obtained in-game object "spider feed"
        Static blnStallman As Boolean = True        'Turns false if the user has completed the Richard Stallman chat sequence
        Static blnSlain As Boolean = True          'Turns false if the user has slain Richard Stallman for the "spider feed"
        Static blnNorthDoor As Boolean = True       'Checks if the user has access to the Northern door in room 2
        Static intEnd As Integer                    'Stores a value corresponding to which ending the player received

        strGameLog = Me.rtxtGameLog.Text & vbCrLf & vbCrLf

        If strUserWord1 = "help" And strUserWord2 = Nothing Then
            rtxtGameLog.Text = strGameLog & "To play enter simple verb and  noun commands like so:" & vbCrLf & vbCrLf & "look door" & vbCrLf & _
                "open door" & vbCrLf & "push door" & vbCrLf & vbCrLf & "Try different words and combinations to do different things!" & vbCrLf _
                & vbCrLf & "You may move from room to room using north, east, south, and west." & vbCrLf & _
                "For example, 'go north' will take you north." & vbCrLf & "Good luck!"
        ElseIf strUserWord1 = "restart" Then
            Call Restart(blnNorthDoor, blnSlain, blnStallman, blnSpiderFeed)
        ElseIf strUserWord1 = "clear" And strUserWord2 = "log" Then
            rtxtGameLog.Text = ""
        End If

        Select Case intUserRoom
            Case 0, Nothing
                Call Room0(strUserWord1, strUserWord2, strGameLog)
            Case 1
                Call Room1(strUserWord1, strUserWord2, strGameLog)
            Case 2
                Call Room2(strUserWord1, strUserWord2, strGameLog, blnSpiderFeed, blnNorthDoor, blnSlain)
            Case 3
                Call Room3(strUserWord1, strUserWord2, strGameLog, blnSpiderFeed, blnStallman, blnSlain, blnNorthDoor)
            Case 4
                Call Room4(strUserWord1, strUserWord2, strGameLog, blnSpiderFeed, blnStallman)
            Case 5
                Call Room5(strUserWord1, strUserWord2, strGameLog, blnSlain, intEnd)
            Case 6
                Call Room6(strGameLog, strUserWord1, intEnd)
        End Select

        strNewGameLog = Me.rtxtGameLog.Text & vbCrLf & vbCrLf       'gets a new string of the game log

        If strGameLog = strNewGameLog Then       'checks if an interpretation was found. 
            Me.rtxtGameLog.Text = strGameLog & "Sorry, I don't understand that command. Type 'help' for instructions."
        End If

        rtxtGameLog.SelectionStart = rtxtGameLog.TextLength     'sets cursor to the end of the textbox
        rtxtGameLog.ScrollToCaret()     'scrolls to position of cursor (ie scrolls to the bottom of the textbox)


    End Sub

    Private Sub btnClearLog_Click(sender As Object, e As System.EventArgs) Handles btnClearLog.Click

        rtxtGameLog.Text = Nothing

    End Sub

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'FOR THE FOLLOWING 1 through 5 "Room" Sub Procedures
    'TextInterpret() calls on one of the following "Room" procedures to search for an interpretation
    'that is specific to the in-game position of the user. An "Else...If" Procedure checks for a certain keywords
    'in either strUserWord1 or strUserWord2. Then, a "Select Case...Case" statement is used within each ElseIf to check
    'for a corresponding word that will generate a text response in rtxtGameLog. As well intScore or intRoom may be
    'altered, or an event will occur that will change a boolean in the parameters.
    'These booleans are used to keep track of whether certain events that have or have not happened in the game. For instance,
    'the user cannot pick up a certain item twice, so the boolean switches to false if the event ocurrs.
    'Thus, "If...Else" statements are also implemented to check the status of a boolean for these interpretations.
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    '~~~~~~~~~~~~~~~~~~~~~~~~~~
    'Leads to Room 1.
    '~~~~~~~~~~~~~~~~~~~~~~~~~~
    Sub Room0(ByVal strUserWord1 As String, ByVal strUserWord2 As String, ByVal strGameLog As String)

        If strUserWord1 = "start" Then
            Me.rtxtGameLog.Text = "Welcome to ‘Trapped In a Closet', the only closet escape simulator on the internet." & _
                vbCrLf & vbCrLf & "To play enter simple verb and  noun commands like so:" & vbCrLf & vbCrLf & "look door" & vbCrLf & _
                "open door" & vbCrLf & "push door" & vbCrLf & vbCrLf & "Try different words and combinations to do different things!" & vbCrLf _
                & vbCrLf & "You may move from room to room using north, east, south, and west." & vbCrLf & _
                "For example, 'go north' will take you north." & vbCrLf & "Good luck!" & vbCrLf & vbCrLf & "You find yourself trapped in a closet. " & _
                "Looking around, it appears to be dark and empty, you wish that you were not here." & vbCrLf & vbCrLf & _
                "There is a door in front of you. Opening it might be a good idea, possibly."
            intUserRoom = 1
            Me.picRoom.Image = Image.FromFile("closet.png")
        End If

    End Sub

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'Leads to room 2.
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Sub Room1(ByVal strUserWord1 As String, ByVal strUserWord2 As String, ByVal strGameLog As String)

        If strUserWord1 = "look" Then
            Select Case strUserWord2
                Case Nothing, "pull"
                    Me.rtxtGameLog.Text = strGameLog & "Looking around, it appears to be dark and empty. You wish that you were not here." & _
                    vbCrLf & vbCrLf & "There is a door in front of you. Opening it might be a good idea, possibly."
                Case "door"
                    Me.rtxtGameLog.Text = strGameLog & "You observe the door in front of you. You safely conclude it IS a door."
            End Select
        ElseIf strUserWord1 = "go" Then
            Select Case strUserWord2
                Case "east", "west", "south"
                    Me.rtxtGameLog.Text = strGameLog & "A wall obstructs your path."
                Case "north"
                    Me.rtxtGameLog.Text = strGameLog & "A door obstructs your path."
            End Select
        ElseIf strUserWord2 = "door" Then
            Select Case strUserWord1
                Case "open", "pull"
                    Me.rtxtGameLog.Text = strGameLog & "You twist the doorknob and try PULLING on the door, but it won't budge."
                Case "push"
                    Me.rtxtGameLog.Text = strGameLog & "Oh, right. You PUSH the door open and go through it." & vbCrLf _
                    & "You are in the small room of an apartment. Facing north, there is a door and a painting on the wall." & vbCrLf _
                    & "There is another door to the west. To the east is a window."
                    Me.picRoom.Image = Image.FromFile("room2a.png")
                    intUserRoom = 2
                Case "break"
                    Me.rtxtGameLog.Text = strGameLog & "You attempt to break down the door with your fists. Have any other good ideas, Houdini?" & _
                    vbCrLf & "-5 points for shameful failure."
                    intPoints = intPoints - 5
            End Select
        End If

    End Sub

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'Leads to room 3. Leads to room 5 if (blnNorthDoor = True)
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Sub Room2(ByVal strUserWord1 As String, ByVal strUserWord2 As String, ByVal strGameLog As String, ByVal blnSpiderFeed As Boolean, _
              ByRef blnNorthDoor As Boolean, ByVal blnSlain As Boolean)

        If strUserWord1 = "look" Then
            Select Case strUserWord2
                Case "door"
                    Me.rtxtGameLog.Text = strGameLog & "There are 2 doors. They are very door-ish."
                Case "window"
                    Me.rtxtGameLog.Text = strGameLog & "You look out the window and see that you are on the 10th floor " & _
                        " of an Edmonton apartment building." & vbCrLf & "It is a nice sunny day out."
                Case "painting", "picture", "art"
                    Me.rtxtGameLog.Text = strGameLog & "You walk up to inspect the painting only to find that it was actually a GRUE. " & _
                    "You manage to escape, but lose a point for not being more conscious of your surroundings."
                    intPoints = intPoints - 1
                Case "room"
                    Me.rtxtGameLog.Text = strGameLog & "You are in the small room of an apartment. Facing north, there is a door " & _
                        "and a painting on the wall." & vbCrLf & "There is another door to the west. To the east is a window."
                    If blnNorthDoor Then
                        Me.picRoom.Image = Image.FromFile("room2a.png")
                    Else
                        Me.picRoom.Image = Image.FromFile("room2.png")
                    End If
                Case "lightbulb"
                    Me.rtxtGameLog.Text = strGameLog & "You look at the lightbulb as it emits light, this is useful because " & _
                        "your eyes are sensitive to photo radiation."
                Case "spider"
                    If blnNorthDoor Then
                        Me.rtxtGameLog.Text = strGameLog & "You look at the spider, it has eight legs and is the embodiment of death."
                    Else
                        Me.rtxtGameLog.Text = strGameLog & "The spider has fled."
                    End If
            End Select

        ElseIf strUserWord1 = "go" Then
            Select Case strUserWord2
                Case "north"
                    If blnNorthDoor = True Then
                        Me.rtxtGameLog.Text = strGameLog & "You almost put your hand on the door knob when you notice " & _
                            " there's a SPIDER on it. North is not an option for now." & vbCrLf & "The spider takes a few of your points."
                        Me.picRoom.Image = Image.FromFile("spider_knob.png")
                        intPoints = intPoints - 3
                    ElseIf blnNorthDoor = False Then
                        Me.rtxtGameLog.Text = strGameLog & "You go through the door to the north." & vbCrLf & "You are now in a hallway " & _
                            "with an elevator immediately north of you. Down the hall to the east is a stairwell."
                        intUserRoom = 5
                        Me.picRoom.Image = Image.FromFile("hall.png")
                    End If
                Case "south"
                    Me.rtxtGameLog.Text = strGameLog & "You decide going back in to the closet would be counter-productive, " & _
                        "so you stay put."
                Case "east"
                    Me.rtxtGameLog.Text = strGameLog & "The window's glass pane obstructs your path."
                Case "west"
                    Me.rtxtGameLog.Text = strGameLog & "You go through the door to the west. You are in an empty room aside from " & _
                        " Richard Stallman, whose portly shadow stands before you. He appears to be crafting macoroni pictures. " & _
                        "You notice there is some strange writing on the wall. " & " There is a door to the east which you entered from."
                    intUserRoom = 3
                    If blnSlain Then
                        Me.picRoom.Image = Image.FromFile("room3.png")
                    Else
                        Me.picRoom.Image = Image.FromFile("room3a.png")
                    End If
            End Select

        ElseIf strUserWord2 = "spider" Then
            If blnNorthDoor Then
                Select Case strUserWord1
                    Case "kill", "squash", "hit", "smash", "slay", "defenestrate"
                        Me.rtxtGameLog.Text = strGameLog & "It might be one of those lethal jumping spiders, so you decide that it is best " & _
                            " to keep your distance from it."
                    Case "talk"
                        Me.rtxtGameLog.Text = strGameLog & "You attempt to reason with the spider, but it only seems gets angrier."
                    Case "eat"
                        Me.rtxtGameLog.Text = strGameLog & "Absolutely not."
                    Case "feed"
                        If blnSpiderFeed = False Then
                            Me.rtxtGameLog.Text = strGameLog & "You feed the spider. Satisfied with its meal, it goes away. " & vbCrLf & _
                            "You can now enter the northern door!"
                            blnNorthDoor = False
                            Me.picRoom.Image = Image.FromFile("room2.png")
                        End If
                End Select
            Else
                Me.rtxtGameLog.Text = strGameLog & "The spider has retreated, for now."
            End If

        ElseIf strUserWord2 = "window" Then
            Select Case strUserWord1
                Case "open"
                    Me.rtxtGameLog.Text = strGameLog & "The window is stuck shut with feces."
                Case "break", "smash", "destroy"
                    Me.rtxtGameLog.Text = strGameLog & "You hurt your fists trying to break the window open." & vbCrLf & _
                        "-4 points for hurting your hands."
                    intPoints = intPoints - 4
                Case "take"
                    Me.rtxtGameLog.Text = strGameLog & "You attempt to dismantle the window from it's frame. You are unsuccessful."
            End Select

        ElseIf strUserWord1 = "take" Then
            Select Case strUserWord2
                Case "door"
                    Me.rtxtGameLog.Text = strGameLog & "The doors are stuck do their frames with hinge-like mechanisms."
                Case "lightbulb"
                    Me.rtxtGameLog.Text = strGameLog & "You are not tall enough to reach the lightbulb."
                Case "painting"
                    Me.rtxtGameLog.Text = strGameLog & "Why do you have to take everything you see? Why can't you just leave it alone?"
            End Select
        End If

    End Sub

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'Leads to room 4 (chat sequence) or back to room 2.
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Sub Room3(ByVal strUserWord1 As String, ByVal strUserWord2 As String, ByVal strGamelog As String, ByRef blnSpiderFeed As Boolean, _
              ByRef blnStallman As Boolean, ByRef blnSlain As Boolean, ByVal blnSpider As Boolean)

        Static blnPicture As Boolean = True         'Turns false if user has acquired in-game picture

        If strUserWord2 = "richard" Or strUserWord2 = "stallman" Or strUserWord2 = "richardstallman" Then
            If blnStallman Then         'user has not completed chat sequence
                Select Case strUserWord1
                    Case "talk"
                        Me.rtxtGameLog.Text = strGamelog & "'Hello', you say to Richard Stallman." & vbCrLf & _
                            "He turns to you and speaks." & vbCrLf & "'YOU HAVE ENTERED STALLMAN'S DOMAIN.'" & vbCrLf & _
                            "'DO YOU SUPPORT USER'S FREEDOM?'"
                        intUserRoom = 4
                    Case "kill", "murder", "destroy", "fight", "slay", "hit"
                        Me.rtxtGameLog.Text = strGamelog & "You have slain Richard Stallman!" & vbCrLf & _
                        "You find a half eaten bag of " & "'Spider Feed' in his beard. You think it might come in handy at some point." & _
                        vbCrLf & "You lose 50 points for commiting a felony."
                        intPoints = intPoints - 50
                        blnSpiderFeed = False           'Indicates user has acquired the spider feed.
                        blnSlain = False               'Indicates user has slain Richard Stallman to complete chat sequence
                        blnStallman = False             'Indicates user has completed chat sequence
                        Me.picRoom.Image = Image.FromFile("room3a.png")
                    Case "defenestrate"
                        Me.rtxtGameLog.Text = strGamelog & "There are no windows and somehow you don't think that would be a " & _
                        " productive use of your time."
                    Case "look"
                        Me.rtxtGameLog.Text = strGamelog & "You look at Richard Stallman. He stares at you instensely."
                End Select
            ElseIf blnSlain = True Then
                Me.rtxtGameLog.Text = strGamelog & "He is working on his macaroni pictures and it looks like he doesn't want " & _
                    " to be disturbed."
            Else
                Me.rtxtGameLog.Text = strGamelog & "You can't do that anymore."
            End If

        ElseIf strUserWord1 = "install" And strUserWord2 = "gentoo" Then
            If blnSlain Then
                If blnPicture Then
                    Me.rtxtGameLog.Text = strGamelog & "You install gentoo." & vbCrLf & "'Thank you for installing gentoo, please take this " & _
                    "as a token of my gratitude." & vbCrLf & "You have received STALLMAN'S MACARONI ART', worth 100 points!"
                    intPoints = intPoints + 100
                    blnPicture = False
                Else
                    Me.rtxtGameLog.Text = strGamelog & "You already did that."
                End If
            Else
                Me.rtxtGameLog.Text = strGamelog & "You can't do that anymore."
            End If

        ElseIf strUserWord1 = "look" Then
            Select Case strUserWord2
                Case "stallman", "richard", "richardstallman"

                Case "room"
                    Me.rtxtGameLog.Text = strGamelog & "You are in an empty room aside from " & _
                        " Richard Stallman, whose portly shadow stands before you. He appears to be crafting macoroni pictures. " & _
                        "You notice there is some strange writing on the wall. " & " There is a door to the east which you entered from."
                Case "writing", "wall"
                    Me.rtxtGameLog.Text = strGamelog & "You notice there is some writing smeared on the wall" & _
                        " in some mysterious red substance, but you decide it's best not to think too much in to it. " & _
                        "The text reads 'Install Gento..'. It looks like whoever wrote it ran out before they could finish."
            End Select

        ElseIf strUserWord1 = "go" Then
            Select Case strUserWord2
                Case "north", "south", "west"
                    Me.rtxtGameLog.Text = strGamelog & "A wall obstructs your path."
                Case "east"
                    Me.rtxtGameLog.Text = strGamelog & "You walk back through the eastern door."
                    If blnSpider Then
                        Me.picRoom.Image = Image.FromFile("room2a.png")
                    Else
                        Me.picRoom.Image = Image.FromFile("room2.png")
                    End If
                    intUserRoom = 2
            End Select

        End If

    End Sub

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'Leads to room 3 (ends chat sequence).
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Sub Room4(ByVal strUserWord1 As String, strUserWord2 As String, ByVal strGameLog As String, _
                 ByRef blnStallman As Boolean, ByRef blnSpiderFeed As Boolean)

        If strUserWord1 = "yes" Or strUserWord2 = "yes" Then
            rtxtGameLog.Text = strGameLog & "'Thank-you comrade. You may have this, when you need it you will know.'" & vbCrLf & _
            "Richard Stallman hands you a half-eaten bag of 'Spider Feed'. This may come in handy later, you think." & vbCrLf & _
            "'Oh, and watch out for the stairs, man.'"
            blnStallman = False
            blnSpiderFeed = False
            intUserRoom = 3

        ElseIf strUserWord1 = "no" Or strUserWord2 = "no" Then
            rtxtGameLog.Text = strGameLog & "'That's terrible!'" & vbCrLf & _
            "You lost 5 points."
            intPoints = intPoints - 5
            intUserRoom = 3
        End If

    End Sub

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'Leads to room 6 (score display) or back to room 4.
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Sub Room5(ByVal strUserWord1 As String, ByVal strUserWord2 As String, ByVal strGameLog As String, ByVal blnSlain As Boolean, _
              ByRef intEnd As Integer)

        Static blnMasterSword As Boolean = True     'Turns false if the user obtains in-game object "Master Sword"

        If strUserWord2 = "elevator" Then

            Select Case strUserWord2
                Case "open", "break"
                    Me.rtxtGameLog.Text = strGameLog & "You attempt to pry open the elevator doors, but for some reason they are stuck shut"
                Case "go", "enter", "use"
                    Me.rtxtGameLog.Text = strGameLog & "Your path is obstructed by the elevator door."
            End Select

        ElseIf strUserWord1 = "go" Then
            Select Case strUserWord2
                Case "west"
                    If blnMasterSword Then
                        Me.rtxtGameLog.Text = strGameLog & "You go east and find the Master Sword laying on the ground." & _
                            " You wonder how you hadn't noticed that earlier!"
                        blnMasterSword = False
                    Else
                        Me.rtxtGameLog.Text = strGameLog & "You went there already!"
                    End If
                Case "east"
                    If blnSlain Then
                        Me.rtxtGameLog.Text = strGameLog & "You decide to head down the stairwell, and as you do you hear " & _
                            "Richard Stallman's voice as it echoes 'watch out for the stairs, man' in the back of your head. You" & _
                            " take caution and use proper stair practice. You leave the building." & vbCrLf &
                            "Congratulations! You beat Trapped in a Closet, type 'continue' to see your score."
                        Me.picRoom.Image = Image.FromFile("end.png")
                        intUserRoom = 6
                        intEnd = 0
                    Else
                        Me.rtxtGameLog.Text = strGameLog & "You decide to head down the stairwell. Tragically, you were not being" & _
                            " careful and fell down the stairs, yet another victim of descending perpedicular intersections." & _
                            " As you fall, you think about all of the terrible things you did, and how you deserved this." & _
                            vbCrLf & "You lose all of your points." & vbCrLf & "Enter in 'continue' to see your score."
                        Me.picRoom.Image = Image.FromFile("end.png")
                        If intPoints > 0 Then
                            intPoints = 0
                        End If
                        intUserRoom = 6
                        intEnd = 1
                    End If
                Case "north"
                    Me.rtxtGameLog.Text = strGameLog & "An elevator door obstructs your path."
                Case "south"
                    Me.rtxtGameLog.Text = strGameLog & "You enter the south door leading back to the apartment room." & _
                        " You think to yourself that you are not very good at these text games."
                    Me.picRoom.Image = Image.FromFile("room2.png")
                    intUserRoom = 2
            End Select

        ElseIf strUserWord2 = "button" Then
            Select Case strUserWord1
                Case "press", "push", "poke"
                    If blnMasterSword Then
                        Me.rtxtGameLog.Text = strGameLog & "You press the button next to the elevator door. You wait for a moment, and" & _
                            " then with a 'DING' the doors slide open and--" & vbCrLf & "YOU HAVE BEEN EATEN BY A GRUE." & vbCrLf & _
                            "Enter 'continue' to see your score."
                        Me.picRoom.Image = Image.FromFile("end.png")
                        intUserRoom = 6
                        intEnd = 2
                    Else
                        Me.rtxtGameLog.Text = strGameLog & "You press the button next to the elevator door. You wait for a moment, and " & _
                            "then with a 'DING' the doors slide open and you are immediately attacked by a GRUE. Thankfully," & _
                            " you have the master sword on you and slay it." & vbCrLf & "You get 1000 points, and MAD STREET CRED." & _
                            vbCrLf & vbCrLf & "Congratulations on beating 'Trapped in a Closet'! Enter 'continue' to see your score."
                        Me.picRoom.Image = Image.FromFile("end.png")
                        intUserRoom = 6
                        intPoints = intPoints + 1000
                        intEnd = 3
                    End If
            End Select
        End If

    End Sub

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'pre: the user has won the game and has entered "continue"
    'post: the user is presented with their game score.
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Sub Room6(ByRef strGameLog As String, ByVal strUserWord1 As String, ByVal intEnd As Integer)

        Dim strEnd As String            'Text that is displayed for user corresponding to which end he received.

        Select Case intEnd
            Case 0
                strEnd = "OKAY END"
            Case 1
                strEnd = "BAD END"
            Case 2
                strEnd = "GRUE END"
            Case 3
                strEnd = "GOOD END"
        End Select

        If strUserWord1 = "continue" Then

            Me.rtxtGameLog.Text = strGameLog & "RESULTS:" & vbCrLf & "You got the " & strEnd & vbCrLf & _
                "You got " & intPoints & " out of a possible 1,100 points!"

            strGameLog = Me.rtxtGameLog.Text
            Me.rtxtGameLog.Text = strGameLog & vbCrLf & "Type 'restart' to play again. Go to [Menu -> End Program] to close the game."

        End If

    End Sub

    Private Sub mnuInstructions_Click(sender As Object, e As System.EventArgs) Handles mnuInstructions.Click

        MessageBox.Show("Welcome to 'Trapped in a Closet'. This is a short text-based game that depends only upon your lexical input in " & _
                        "to the text box in the bottom left. I have provided the game with some immersing artwork that took me " & _
                        "a few minutes in paint. There are some unusual elements to this game, however your objectives are " & _
                        "fairly straight-forward. At any time you may type in 'help' for directions on how to make your inputs." & _
                        vbCrLf & "Enjoy, and watch out for GRUES.", "Instructions")

    End Sub

    Private Sub mnuEndProgram_Click(sender As Object, e As System.EventArgs) Handles mnuEndProgram.Click

        End

    End Sub

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'Calls on Textinterpret() with the string "restart" which will trigger TextInterpret() to call on
    'Restart() with the necessary values to be reset.
    'post: TextInterpret is called with parameters ("restart", Nothing).
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Private Sub mnuRestart_Click(sender As Object, e As System.EventArgs) Handles mnuRestart.Click

        Dim strRestart As String

        strRestart = "restart"

        Call TextInterpret(strRestart, Nothing)

    End Sub

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    'Resets the game back to it's original state at start-up, and notifies the user.
    '
    'post: all booleans marking game progress are reset back to true. intUserPoints and
    'intUserRoom are set to Nothing. The game log is cleared.
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    Sub Restart(ByRef blnNorthDoor As Boolean, ByRef blnSlain As Boolean, ByRef blnStallman As Boolean, _
                ByRef blnSpiderFeed As Boolean)

        blnNorthDoor = True
        blnSlain = True
        blnStallman = True
        blnSpiderFeed = True
        intUserRoom = Nothing
        intPoints = Nothing

        Me.picRoom.Image = Nothing

        Me.rtxtGameLog.Text = "Game restarted." & vbCrLf & "Press 'start' to begin"

    End Sub

End Class
