/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here.
	// For complete reference see:
	// http://docs.ckeditor.com/#!/api/CKEDITOR.config

	// The toolbar groups arrangement, optimized for two toolbar rows.
	config.toolbarGroups = [
		//{ name: 'clipboard',   groups: [ 'clipboard', 'undo' ] },
		{ name: 'editing',     groups: [ 'find', 'selection', 'spellchecker' ] },
		{ name: 'links' },
		{ name: 'insert' },
		{ name: 'forms' },
		{ name: 'document', groups: ['mode', 'document', 'doctools'] },
		{ name: 'others' },
//		'/',
		{ name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ] },
		{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'] },
        { name: 'tools' },
		{ name: 'styles' },
		{ name: 'colors' },
		//{ name: 'about' }
	];

	// Remove some buttons provided by the standard plugins, which are
	// not needed in the Standard(s) toolbar.
	//config.removeButtons = 'Underline,Subscript,Superscript';
	config.removeButtons = 'Cut,Copy,Undo,Redo,Source,Subscript,Superscript,Styles,Maximize,SpellChecker,Anchor,Scayt';

	// Set the most common block elements.
	config.format_tags = 'p;h1;h2;h3';

	config.coreStyles_bold = { element: 'b', overrides: 'strong' };

	// Simplify the dialog windows.
	config.removeDialogTabs = 'image:advanced;link:advanced';

	config.height = 100;

	config.extraPlugins = 'autogrow';
	config.autoGrow_minHeight = 100;
	config.autoGrow_maxHeight = 600;

	config.extraPlugins += ',tableresize';

	config.language = 'ru';
	//config.filebrowserBrowseUrl = '/Scripts/filemanager/index.html';

};
