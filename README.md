# photostudio
Steelworks PhotoAlbum and PhotoStudio.

This project is a personal photo album management system.

The photo album is organised and documented with an XML file for each year and for each event.
The photos are optionally named with date/time stamps - this allows pictures to be displayed in order,
and for the timeline to be displayed in the events of the album. By naming the pictures as date/time stamps,
the Album software is not dependent on the file system date and time of each picture (which might change
on editing), nor on any file metadata.

The PhotoStudio program manages the album: using it, you can add new years and events to the album,
and rename image files with date/time stamps. You can even adjust the date/time stamps by an hour forward
or back, to cater for incorrect time in the camera due to a change in daylight savings time.

The PhotoAlbum software is the viewing application: it does not provide any editing capabilities.


###Rev_2_048      16-JUN-2016

PhotoStudio and PhotoAlbum relied upon specific paths to files and did not cope well with the photo
album being moved to a different directory path. If PhotoAlbum fails to load the album on start-up,
it now prompts the user to navigate to and select the album.xml file.

###Rev_2_047      13-JUN-2016

First version in GitHub.

###Rev_2_046      19-JUL-2013

First version built using VS 2012, first version in SVN.

###Rev_2_045      20-NOV-2011

The Registry library is now in use. The Properties Settings have been removed
from the PhotoStudio and PhotoAlbum projects.

###Rev_2_044      30-APR-2011

Added the Registry library and Registry class. This is a first step in
migrating from using Properties Settings to the Windows Registry to preserve
settings between program runs - no functionality implemented as yet.

###Rev_2_043      30-APR-2011

[1] Project migrated from Visual Studio 2008 to 2010.

[2] When adding existing slide show, the slide show title dialog is now
	shown, pre-populated with the existing full title of the slide show.

###Rev_2_042      06-FEB-2011

PhotoStudio edit form is now resizeable.

###Rev_2_041      05-FEB-2011

PhotoAlbum main form now has better behaviour with regard to resizing. The
toolbar is docked to the top of the form, the tab control is docked to fill 
the form. The problem experienced in preparing Rev_2_040 is that the toolbar 
was "in front of" the tab control, and hence obscured the top part of it.
This is resolved by exercising "bring to front" on the tab control.

###Rev_2_040      05-FEB-2011

PhotoAlbum main form now resizeable. There is now a single row of scrollable
tabs instead of a multiple rows, as many of needed to accommodate all tabs.

I wanted the Events list box to grow when the form grows, and for the 
controls on the right hand side to reposition themselves according to the
form size; but this has proved difficult and is outstanding.

###Rev_2_039      16-JAN-2011

PhotoAlbum main form resized, as an increased number of tabs was causing
overlap of the buttons on the form.

###Rev_2_038      01-JAN-2011

PhotoStudio bug-fix: the DateName facility failed due to two pictures having
the same timestamp (camera had been used in burst mode). Picture filenames
can now have a suffix letter to resolve such situations.

###Rev_2_037      10-DEC-2010

PhotoAlbum: it was taking a long time for the program to start, as it would
load the whole album comprising many different files before displaying the
main form. Now each year is loaded on demand, for better responsiveness.

###Rev_2_036      09-DEC-2010

Removed the step-by-step XML encode/decode that was made redundant in
###Rev_2_027. No functional changes, except that configuring the old load/save
methods back in is no longer possible.

###Rev_2_035      31-OCT-2010

PhotoStudio bug-fixes:
[1] When creating a new slide-show, the user was asked for a title, but the
    title was lost and defaulted to "A most extraordinary day": now resolved.

[2] When creating a new slide-show, if no images were found in the supplied
    folder, then the show was created but this caused exceptions later on.
    Now if this is the case, the show is not created and the user is warned:
    it is not valid to create a show containing no slides

[3] When adding a new year or event list to the main form, the whole list is
	now sorted into order (previously, if inserting a year which was out of
	sync, it would be positioned at the end of the list). Outstanding: the
	tabs on the AlbumForm are not sorted until the next program run.

###Rev_2_034      19-AUG-2010

[1] PhotoStudio bug-fix: the XML serialiser introduced in Rev_2_027 was
    recognising captions with tags of <string> instead of the original
    <line>. This meant that old shows with <line>s which were loaded
    apparently had no captions, and if they were re-saved then the captions
    were permanently lost. Ouch! The <line> tag has now been restored.

[2] Whenever an event list was modified and saved, it automatically recursed
    and saved all the slideshows as well. This is unnecessary, and in
    conjunction with the above bug, many captions have been lost. Slideshows
    are no longer saved unnecessarily.

###Rev_2_033      15-AUG-2010

SlideShowEditor: now able to add the missing slides to the show.

###Rev_2_032      15-AUG-2010

SlideShowEditor will now recognise and report image files in the slide show
folder which are not included as part of the slide show (on the status
strip).

Outstanding: there is a menu item to add the pictures to the slide show - not
implemented yet.

###Rev_2_031      31-JUL-2010

Reworked the SlideShowEditor, which shows five thumbnails in addition to a
main picture, so that it shunts pictures along as the slides are scrolled
left or right instead of reloading all pictures from scratch. This speeds up
the general operation of the editor - especially noticeable for slides of
larger images.

###Rev_2_030      31-JUL-2010

Bug-fixes: 

[1] Implemented the Rev_2_026 bug-fix properly - now loading images directly
from file instead of stream again, but disposing of image resources after
use. This simplifies the SlideShowEditor code.

[2] Resolve program crash if editing slide show and deleting all images.

[3] Resolve SlideShow crash if slide had no caption at all.

###Rev_2_029      28-JUL-2010

PhotoStudio and PhotoAlbum main form now display version information - when
a tagged version is checked out of CVS.

###Rev_2_028      25-JUL-2010

SlideShow bug-fix: hiding the cursor was only working for the first slide
show in any program run.

###Rev_2_027      24-JUL-2010

Album, EventList, and SlideShow classes rewritten to use XML Serialiser to
load and save parts of the album, replacing the step-by-step encode/decode
the XML process.

###Rev_2_026      24-OCT-2009

Bug-fix: attempting to delete files using the SlideShowEditor did not work:
now load images via a stream instead of explicitly from file to resolve this.

Outstanding: the same fix should be applied to the SlideViewerForm, otherwise
the same problem will arise if the slide show has been viewed prior to 
editing.

###Rev_2_025      14-JUN-2009

Replaced the discrete buttons on the AlbumForm toolstrip with proper
toolstrip buttons. When moving events up and down the EventList, the event
which was selected and moved now remains the selected event (previously, the
same position remained selected) - this is the more natural behaviour when
using the toolstrip to rearrange the EventList.

###Rev_2_024      13-JUN-2009

Added some buttons to the toolstrip in the AlbumForm - as an alternative to
the context menu. Only a selection have been added so far, and have not been
tested. Outstanding: these buttons need tool tips.

###Rev_2_023      13-JUN-2009

The event label (slide show caption) appearing above the picture on the
AlbumForm now wraps when appropriate - previously a long label would run over
the edge of the form.

###Rev_2_022      12-JUN-2009

SlideShowEditor now has a Delete button: images can be removed from the
slide show, especially useful if the image doesn't exist because it was
deleted outside of the show.

Outstanding: the progress gauge in the SlideShowEditor form does not take
into account slides that have been deleted, so that as the forward button is
progressively clicked, the gauge does not reach 100% by the end of the show.

###Rev_2_021      07-JUN-2009

Bug-fixes:
[1] SlideViewerForm was crashing if unrecognised key was pressed (it was
    recursive). Particularly problematic because numeric keypad + and - were
    used to control the speed of the slide transitions - for the sake of
    keyboards without a numeric keypad (like most laptops), plus and minus 
    (unshifted) on the main keyboard are now recognised. Unrecognised keys
    are now ignored, but the default clause within the 
    SlideViewerForm_KeyDown method now contains commented out code to display
    details of the key pressed.
[2] SlideViewerForm was crashing if image file could not be found (common
    problem if slide show is created and then the image set is pruned without
    recreating the slideshow).

Outstanding:
Tried adding some PictureBox.dispose method calls in SlideShowEditor.cs, but
these cause pictures to be blanked - not sure why this is. Commented out for
now.

###Rev_2_020      31-MAY-2009

Can now add "events" that have no slide show to an Event List.

###Rev_2_019      31-MAY-2009

Debug of saving Events Lists and Album: in particular, if a new page in the
album was created, events added, and then the album closed, it was offering
to save the top-level album but not the new page.
Outstanding:
[1] No facility to insert non-slideshow items into the Events List (ie, group
    headings to help with levelling)
[2] Need an easier means of rearranging events in the Events List.
[3] No facility to edit the Events List titles - either the brief title in
    the Events List itself, or of the slide show.

###Rev_2_018      30-MAY-2009

When selecting tabs to change year in the Album, if the Events List for the 
previously selected year has changed, the program should now prompt and
save the Events List. When exiting the Album, if years have been added, the
program should now prompt and save the top-level album.
THIS HAS NOT BEEN TESTED YET.

###Rev_2_017      30-MAY-2009

Can now add a new tab to the AlbumForm, and add events to the empty list.
Outstanding:
[1] New years added in this way must be saved.
[2] New and revised Events lists must be saved.
[3] If a new SlideShow is inserted at an inappropriate place in the AlbumForm
    Listbox, it can be extremely tedious using the context menu to move it
    any distance. Should add tool bar buttons to perform the events of the
    context menu, and ensure that when moving an event it remains selected.
    This would allow an event to be moved by repeatedly clicking on the
    appropriate tool bar button.

###Rev_2_016      27-MAY-2009

A "New" tab added to the AlbumForm tab control, and a CreateNewYear method
added to the class. The method is just a stub at present: it needs to display
a form inviting the user to enter the name of the year.

###Rev_2_015      26-MAY-2009

Can now set the Slideshow title and its name in the EventsList when using
the "Add new" context menu entry in AlbumForm. Outstanding:
[1] An Events list revised through the context menu is not saved
[2] If a new SlideShow is inserted at an inappropriate place in the AlbumForm
    Listbox, it can be extremely tedious using the context menu to move it
    any distance
[3] Need means of adding a new tab (year) to the AlbumForm

###Rev_2_014      25-MAY-2009

Right-clicking on the AlbumForm Events Listbox now causes the appropriate
event to be selected before the context menu is displayed. Outstanding:
[1] An Events list revised through the context menu is not saved
[2] No means of setting the title of a new slide show, nor the entry name to
    appear in the Events listbox
[3] Need means of adding a new tab (year) to the AlbumForm

###Rev_2_013      25-MAY-2009

After performing a context menu action, AlbumForm now remembers the currently
selected event instead of resetting it.

###Rev_2_012      25-MAY-2009

Replaced the "Insert" context menu item on the AlbumForm with "Add Existing"
(existing .xml file) and "Add New" (create .xml file from folder).
Outstanding:
[1] An Events list revised through the context menu is not saved
[2] No means of setting the title of a new slide show, nor the entry name to
    appear in the Events listbox
[3] Right-clicking on the Events listbox should cause the event nearest the
    mouse pointer to be selected, and the context menu should apply to that
    event: currently the context menu applies to the previously selected
    event, which may not be near the mouse pointer
[4] After using the context menu, the first "playable" item in the Events
    listbox becomes selected: instead the item to which the context menu was
    applied should be selected
[5] Need means of adding a new tab (year) to the AlbumForm

###Rev_2_011      25-MAY-2009

Insert and Delete operations implemented for the context menu in the
AlbumForm. Some further debugging is required.

###Rev_2_010      20-MAY-2009

Can now promote and demote events using the context menu in AlbumForm.
Outstanding:
[1] The revised Events list is not saved
[2] The menu must be extended to cater for insertion and deletion of events
[3] Right-clicking on the Events listbox should cause the event nearest the
    mouse pointer to be selected, and the context menu should apply to that
    event: currently the context menu applies to the previously selected
    event, which may not be near the mouse pointer.
[4] After using the context menu, the first "playable" item in the Events
    listbox becomes selected: instead the item to which the context menu was
    applied should be selected
[5] Need means of adding a new tab (year) to the AlbumForm

###Rev_2_009      20-MAY-2009

Bug-fix: AlbumForm no longer crashes when moving events up and down the list
using the context menu.

###Rev_2_008      20-MAY-2009

The AlbumForm Listbox now has a context menu for editing the Events List.
Outstanding:
[1] Bugs: evident after moving a few events
[2] The revised Events list is not saved
[3] Promotion and demotion of events is not yet implemented
[4] The menu must be extended to cater for insertion and deletion of events.

###Rev_2_007      19-MAY-2009

The SlideShow "Edit" button on the AlbumForm is now visible when the form is
displayed from within PhotoStudio, but not when the form is displayed from
within PhotoAlbum.

###Rev_2_006      19-MAY-2009

[1] When abandoning a slide show prematurely, the mouse pointer is made
    visible (previously, it was left invisible when returning to the 
    AlbumForm or PhotoStudio FormMain).
[2] SlideShowEditor form now correctly splits captions over the required
    lines (was previously concatenating the lines of the caption into a 
    single line).
[3] SlideShowEditor form now recognises when captions have been modified, and
    will save the changes (after confirmation by the user).

Outstanding:
[1] No means of adding a title to the slideshow.
[2] Cannot delete slides from a slideshow.
[3] Cannot edit the list of events in an EventsForm, or the PhotoAlbum.
    
###Rev_2_005      16-MAY-2009

When creating a slideshow in PhotoStudio, confirmation is requested before
overwriting an existing .xml file.

###Rev_2_004      12-MAY-2009

PhotoStudio "Create Slideshow" now uses full date/time as caption if the day
for each slide is different to that of the previous one, otherwise
abbreviated date/time. Outstanding:
[1] No means of adding a title to the slideshow.
[2] Should request confirmation if "Create Slideshow" will overwrite an
    existing .xml slideshow.

###Rev_2_003      10-MAY-2009

PhotoStudio "Create Slideshow" now includes date/time captions where image
filenames have been "datenamed". Outstanding:
[1] Would like intelligence to shorten the caption for successive photos on
    the same day.
[2] No means of adding a title to the slideshow.

###Rev_2_002      09-MAY-2009

PhotoStudio: new "Create slideshow" context menu item when right-clicking on
a folder. Outstanding:
[1] Fails to add the required captions to the slideshow
[2] No means of adding a title to the slideshow

###Rev_2_001      07-MAY-2009

Mouse pointer is now hidden whilst over SlideViewerForm, unless the mouse
moves, in which case it becomes visible for one second.

###Rev_2_000      04-MAY-2009

More keystrokes supported to control the slide show:
 Left arrow		Previous slide
 Right arrow	Next slide
 +				Speed up the slide transitions
 -				Slow down the slide transitions
 C				Toggle captions on and off
 Escape			Terminate the slide show

When adjusting the speed or toggling captions, controls to show the current
state are displayed for one second.

Major revision number chosen, to reflect what is considered to be the first 
usable PhotoAlbum.

###Rev_1_073      04-MAY-2009

SlideViewerForm now responds to keystrokes to speed up or slow down the show.

###Rev_1_072      04-MAY-2009

PhotoAlbum main form was not closing after the user terminated the album. It
now closes half a second after the AlbumForm is closed.

###Rev_1_071      04-MAY-2009

Renamed PhotoAlbum.AlbumForm class to PhotoAlbum.FormMain, to avoid confusion
with the SlideShow.AlbumForm class.

###Rev_1_070      04-MAY-2009

Added system properties to PhotoAlbum and PhotoStudio to control speed of
slide show, and whether captions are to be displayed. Problem: these values
seem to be stuck on 2000ms and true, regardless of actual settings.

###Rev_1_069      03-MAY-2009

SlideViewerForm caption text box: lines of caption are now split for better
reading if longer than a threshold length.

###Rev_1_068      01-MAY-2009

SlideViewerForm caption box now sized better by using 
TextRenderer.MeasureText instead of Graphics.MeasureString.
Outstanding: some lines in the original slide captions need to be split into 
multiple lines in the text box.

###Rev_1_067      30-APR-2009

Sizing of caption box on SlideViewerForm is better: the setting of the
TextBox.SetBounds property in the SlideViewerForm.Resize method appears to
have been the culprit for the main problem. However, the text box is still
generally too small.

###Rev_1_066      28-APR-2009

Bug-fixes: Slide show occasionally crashed when being played a second time,
and was failing to reset to the beginning if it did play. Outstanding: the
caption text box in the SlideViewerForm is not being correctly sized to fit
the caption.

###Rev_1_065      25-APR-2009

The caption on the SlideViewerForm is now held in a text box rather than a
label. The text box automatically resizes to fit the caption - however, some
further debug is required to get this working properly.

###Rev_1_064      24-APR-2009

[1] SlideShowEditor now has a multi-line caption text box (was previously
    single-line).
[2] HTML conversion bug-fix: quotation marks in captions were converting to
    question marks.
    
Outstanding:
XML slide show files define captions as multiple lines, but they run together
in the SlideShowEditor. The labelPhotoCaption on the SlideViewerForm would be
better as a text box rather than a label.

###Rev_1_063      23-APR-2009

Bug-fixes:
[1] Final picture of the Misc Graham slide show appeared twice due to bug in
    conversion of HTML table with two columns and empty cell as the final
    cell.
[2] Slide shows now terminate again after displaying final slide.

###Rev_1_062      22-APR-2009

Added a progress bar to the SlideShowEditor. Problems:
[1] Would like the progress bar to fill the ToolStripStatusBar.
[2] SlideShowEditor shows final picture twice.
[3] SlideShow never terminates (sticks on final picture).
	These latter two bugs are odd: looking at Misc\Graham.xml and
	Misc\Dylys.xml, the former shows the final picture duplicated (suggesting
	conversion problem); the latter	does not.

###Rev_1_061      20-APR-2009

SlideShowEditor form now works from within AlbumForm.

###Rev_1_060      20-APR-2009

[1] Scroll buttons on SlideShowEditor form now work.
[2] Edit button added to AlbumForm (intended for use with PhotoStudio only,
    should be hidden for PhotoAlbum application). However, it displays a
    blank form instead of the editor form.

###Rev_1_059      19-APR-2009

Added a slide show editor, currently only accessible by right-clicking the
XML slide show in the main form.
Issues:
[1] Should be accessible from the AlbumForm
[2] Scroll buttons do not work
[3] Need a gauge that indicates proportion of the way through the slides
[4] No facility to save changes to the captions

###Rev_1_058      18-APR-2009

[1] Slide show no longer starts with default picture of Abbey Road.
[2] Slide show was terminating as soon as the final picture was displayed -
    now shows all pictures for equal duration.

###Rev_1_057      17-APR-2009

Changes:
[1] SlideViewerForm now bears the name of the slide show, instead of "Form1".
[2] Bug-fix: a particular slide show failed if it was played more than once.

Outstanding:
[1] Slide show always displays initial picture of 128 Abbey Road - needs to
    be eliminated.
[2] The splash screen form remains open after the album has finished.
[3] Album needs more controls, over the speed of the transitions, and manual
    forward/reverse/terminate.

###Rev_1_056      16-APR-2009

New PhotoAlbum application shows the photo album without using PhotoStudio.
Issue - the underlying form remains open after the album has finished.


###Rev_1_055      15-APR-2009

AlbumForm PictureBox now populated with "favourite" picture from the
currently selected event. For now, the first picture in the slide show is the
favourite: a means of changing the favourite will be implemented later.

###Rev_1_054      14-APR-2009

[1] TabPage controls are now held in a list: more maintainable if controls
    are added.
[2] Year and current event are now displayed on albumForm.

###Rev_1_053      14-APR-2009

Of the outstanding problems in Rev_1_052:
[1] Title should be displayed as part of the albumPictureBox: 
    still outstanding
[2] Fixed: a <hr> tag now resets the indent.
[3] There was no line break in the HTML, leading to the problem. It was more
    appropriate to fix the HTML than to cater for this quirk
    programmatically.
[4] Forms are now modal: by using Form.ShowDialog rather than Form.Show.
[5] Cosmetic and outstanding.

###Rev_1_052      13-APR-2009

Bug-fixes:
[1] HTML conversion of 1995 EventsList was erroneously combining two events.
[2] HTML conversion was dropping final event of events lists.
[3] HTML conversion of 1997 EventsList was erroneously combining the year
    title and the first event.

Outstanding:
[1] Fix [3] above means that the title is no longer displayed as part of the
    EventsList - it should be displayed above the albumPictureBox instead.
[2] In the converted 1998 EventsList, "Park" has become a level too deep.
[3] In the converted 2004 EventsList, the final two Bethel events are
    combined.
[4] When "playing" the album from PhotoStudio, the AlbumForm should be modal
    rather than modeless.
[5] The image icons down the right hand side of the EventsList in the
    AlbumForm do not make it as obvious which events have a slide show as it 
    should be.

###Rev_1_051      12-APR-2009

[1] AlbumForm Events Listbox no longer obscured by tool bar.
[2] AlbumForm Events Listbox is now owner-draw. Buttons work as per
    EventsForm.
Issue: 1995 EventsList badly converted from HTML.
Outstanding: albumPictureBox should be populated with a favourite image.

###Rev_1_050      11-APR-2009

The AlbumForm Events Listbox is now populated with the events of the initial
year, and repopulates each time a tab is selected. Outstanding:
[1] The tool bar obscures the top of the Events Listbox.
[2] Functionality from the EventsForm must be carried over to the AlbumForm:
    owner-draw Listbox, functionality for Play and Close buttons.

###Rev_1_049      11-APR-2009

The Events Listbox and buttons on the AlbumForm are now transcribed to each
tab page as it is selected.

###Rev_1_048      04-APR-2009

PhotoStudio can now "play" an album.xml file. Added an AlbumForm to be
"played". The tab control is populated with the "years" of the album.
Outstanding:
[1] The tab control starts off with two dummy tabs which are not required.
[2] The Events Listbox and buttons on the AlbumForm belong to the first tab: 
    they should belong to the whole tab control, with the Listbox being 
    repopulated as tabs are selected.
[3] Functionality from the EventsForm must be carried over to the AlbumForm.

###Rev_1_047      04-APR-2009

PhotoStudio bug-fix: when converting yearframe.htm, it was saving the
album.xml correctly, but then treated yearframe.htm as a slideshow and
overwrote album.xml with an empty slideshow.

###Rev_1_046      31-MAR-2009

PhotoStudio now handles anchors when converting from HTML.
Issue: when converting an album, the resulting album.xml file is virtually
empty.

###Rev_1_045      28-MAR-2009

Code is in place to populate and save the Album class when converting an HTML
album.
Issue: album conversion fails because slideshow conversion fails when the
original slideshow contains HTML anchors.

###Rev_1_044      27-MAR-2009

Can now "convert" an HTML album: parses the whole HTML album.
Outstanding: the HtmlReader ReadAlbum parses the album, event lists and slide
shows; but does not yet populate the Album class instance.

###Rev_1_043      25-MAR-2009

When playing a slide show from the EventList form, the pictures now play as
intended.

###Rev_1_042      21-MAR-2009

Can now "play" a slide show from the EventList form. Unfortunately, it does
not work properly. The form displays with the default image, but no slide
show follows.

###Rev_1_041      21-MAR-2009

[1] When "playing" an EventList XML file, the Play button on the EventsForm
    now becomes enabled/disabled depending upon whether a slideshow exists
    for the currently selected event.
    
[2] The controlled errors displayed when loading the 2008 event list (see
    Rev_1_39) have been resolved. When converting a slideshow .htm file to
    .xml, empty lines of caption could result in the .xml file. These were
    the subject of the error messages. Empty lines are valid: they could
    represent a new paragraph (an intervening blank line) in the caption; and
    are now handled as such.

###Rev_1_040      21-MAR-2009

When "playing an EventList XML file, a photo icon is included in the listbox
entry if the event has a slideshow.

###Rev_1_039      17-MAR-2009

When "playing" an EventList XML file, events are now displayed in different
styles and colours according to their level. Outstanding:
	1) No attention is paid to whether or not the event has a slide show
	2) Slide shows cannot yet be played
	3) Controlled errors are reported when loading the 2008 event list

###Rev_1_038      14-MAR-2009

PhotoStudio now distinguishes between Album, Event list and Slide show when
right-clicking and selecting Play on an XML file.
Selecting "Play" for an Event list now displays a prototype form containing
the list of events. Outstanding:
	1) No attention is paid to the level of each event
	2) No attention is paid to whether or not the event has a slide show
	3) Slide shows cannot yet be played

###Rev_1_037      11-MAR-2009

Added a SlideShow Album class - not yet tested.

###Rev_1_036      11-MAR-2009

HTML Preprocess class now discards leading newlines: this results in better
levelling for the EventList class.

###Rev_1_035      07-MAR-2009

Added a Relevel method to the EventList class: initially event levels are set
as a measure of the indentation of each level name. The Relevel method maps
the initial set of level values to a sequential set and reassigns the level
for each event.
Problem: The HTML Preprocess class counts newlines as white space and so
converts a single newline to a space character. A leading newline should not
be counted: this is upsetting the set of level values.

###Rev_1_034      07-MAR-2009

Implemented the EventList Load method. Not tested yet - nothing invokes it
yet.

###Rev_1_033      07-MAR-2009

When parsing an Events.htm file, leading "+" symbols are stripped and
incorporated into the event level. Trailing spaces are stripped from event
names.

###Rev_1_032      07-MAR-2009

When parsing HTML slide show, title is now recognised and will be saved in
the XML slide show.

To do:
  Quotes in slide show captions not translated properly when converting from
  HTML to XML.

###Rev_1_031      06-MAR-2009

PhotoStudio can now convert an Events.htm file to Events.xml, also converting
each of the slideshow HTML files that it finds into slideshow XML files, and
including the slideshow paths in the Events.xml.

To do:
  1) Many event names are prefixed with a "+": these should be stripped and
     incorporated into the event level
  2) Event names have trailing spaces: these should be stripped
  3) EventList Load method not implemented yet
  4) When parsing an HTML slide show, the title is ignored and defaults to
     "A most peculiar day"

###Rev_1_030      04-MAR-2009

New HTML Preprocess class to condense white space and resolve HTML escape
sequences. PhotoStudio now converts Events.htm much better, but resulting
Events.xml file still does not include reference to the actual slide show xml
files.

###Rev_1_029      03-MAR-2009

When converting Events.htm, white space is now condensed and used to
determine the event level; however:
  1) Does not recognise HTML escape sequences (starting with ampersand)
  2) The resulting Events.xml file does not yet include reference to the
     actual slide show xml files.

###Rev_1_028      28-FEB-2009

In PhotoStudio, can now right-click convert an Events.htm file. This is
preliminary and not finished yet. Outstanding:
  1) Need to condense white space in event names
  2) The resulting Events.xml file does not yet include reference to the
     actual slide show xml files.

###Rev_1_027      24-FEB-2009

HTMLReader class had been designed for use with a console app and wrote
diagnostics to console output. Its methods now return diagnostics as string
parameters. Code which uses this class has been updated accordingly.

###Rev_1_026      20-FEB-2009

PhotoTest right-click Convert menu item now functional: available when an
HTML file is selected, will convert to XML. Can then right-click Play to
view the new slide show.

###Rev_1_025      19-FEB-2009

PhotoTest program gave warnings when building, now rectified.

###Rev_1_024      19-FEB-2009

PhotoStudio will now start slide show when right-clicking on a .xml file.

###Rev_1_023      18-FEB-2009

Interim check-in: can now right-click on an XML file and play the slide show.
Problem: the SlideViewerForm is not visible!

###Rev_1_022      17-FEB-2009

PhotoStudio ListView context menu is still non-functional, but the available
option in the menu now changes according to the file type selected.

###Rev_1_021      14-FEB-2009

PhotoStudio application recognises XML and HTML files in addition to Photo
files. Non-functional context menu (right-click menu) added to PhotoStudio
ListView control.

###Rev_1_020      11-FEB-2009

Changed PhotoTest program to use SlideShow Load method (to load an XML file)
instead of using the HTML Reader to load from HTML source. Debugged the
SlideShow Load method.

###Rev_1_019      11-FEB-2009

Added a Load method to the SlideShow class, to load from XML file. Not yet
debugged.

###Rev_1_018      07-FEB-2009

New Caption class defined in the SlideShow module. A caption is now a list of
strings (or lines) rather than a single string. ReadSlideShow method of the 
HtmlReader class updated to parse captions into multiple lines. PhotoTest
SlideViewerForm updated to handle multi-line captions.

###Rev_1_017      07-FEB-2009

SlideShow class now has a Save method, to save slide show to XML file.
Converter program now calls the SlideShow Save method.

###Rev_1_016      06-FEB-2009

[1] HtmlReader class was in its own HtmlReader project, now moved into 
    SlideShow project. HtmlReader project has been removed. 
[2] SlideShow project updated to use HtmlReader instead of using the HTML
    class directly.
[3] PhotoTest application updated to use methods of SlideShow to retrieve
    photos on demand, instead of assembling all the photos into an
    independent list.

###Rev_1_015      05-FEB-2009

Added HtmlReader class. Moved most of the code from the Converter program
into HtmlReader class.

BUG: HtmlReader fails when it finds a link with an anchor in it.

###Rev_1_014      04-FEB-2009

Converter program now captures captions from the album pages.

###Rev_1_013      03-FEB-2009

Converter program now takes command line parameters
	Converter album <path to yearframe.htm>
	Converter page <path to single page of album>
thus allowing conversion of a single page or the whole album.

###Rev_1_012      01-FEB-2009

PhotoTest program now displays the file path for each picture as a caption.

###Rev_1_011      30-JAN-2009

Converter program now parses yearframe.htm:
	identifies each of the events.htm files
	parses each events.htm file:
		identifies each slide show file
		parses each slide show file
			identifies each slide

###Rev_1_010      29-JAN-2009

Added a Converter project. This takes a path to the main index of a HTML
slide show (eg, yearframe.htm) as its command line parameter. At present it
just lists the HTML files that it finds, one per year.

###Rev_1_009      28-JAN-2009

PhotoTest now uses the SlideShow class to extract image names from the HTML
album page.

###Rev_1_008      28-JAN-2009

Imported the PhotoTest slide viewer project into PhotoStudio.

###Rev_1_007      27-JAN-2009

New SlideShow library class: loads a slideshow from HTML file into memory.

###Rev_1_006      26-JAN-2009

FindLinks program updated: if supplied "url" starts "http:", it reads from
HTTP, otherwise it reads from file.

###Rev_1_005      26-JAN-2009

Added FindLinks console app (was part of HTML project): does sample parsing
of web page across HTTP.

###Rev_1_004      25-JAN-2009

Added HTML project, copied from
http://www.developer.com/net/csharp/article.php/2230091

###Rev_1_003      07-SEP-2008

Fixed DateName function and functionality: use the LastWriteTime rather than
the CreationTime attribute of each file.

###Rev_1_002      07-SEP-2008

TreeView now displays all drives instead of C:\ only.

###Rev_1_001      07-SEP-2008

First version in CVS: supports PhotoPage, HourPlus, HourMinus functions.


