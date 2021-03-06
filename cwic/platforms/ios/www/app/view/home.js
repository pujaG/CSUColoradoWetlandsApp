/*
 * File: app/view/home.js
 *
 * This file was generated by Sencha Architect version 3.0.4.
 * http://www.sencha.com/products/architect/
 *
 * This file requires use of the Sencha Touch 2.3.x library, under independent license.
 * License of Sencha Architect does not include license for Sencha Touch 2.3.x. For more
 * details see http://www.sencha.com/license or contact license@sencha.com.
 *
 * This file will be auto-generated each and everytime you save your project.
 *
 * Do NOT hand edit this file.
 */

Ext.define('CWIC.view.home', {
    extend: 'Ext.Container',
    alias: 'widget.home1',

    requires: [
        'Ext.Container',
        'Ext.Button',
        'Ext.Img'
    ],

    config: {
        baseCls: 'home',
        fullscreen: true,
        id: 'home',
        padding: '15px',
        scrollable: true,
        layout: {
            type: 'vbox',
            align: 'center',
            pack: 'center'
        },
        items: [
            {
                xtype: 'container',
                baseCls: 'coverTitle',
                docked: 'top',
                html: 'Colorado Wetlands<br />Mobile App'
            },
            {
                xtype: 'button',
                itemId: 'introButton',
                margin: 10,
                width: '200px',
                text: 'Introduction'
            },
            {
                xtype: 'button',
                itemId: 'fieldGuideButton',
                margin: 10,
                width: '200px',
                text: 'Wetland Plants'
            },
            {
                xtype: 'button',
                disabled: false,
                itemId: 'mapsButton',
                margin: 10,
                width: '200px',
                text: 'Wetland Maps'
            },
            {
                xtype: 'button',
                disabled: false,
                itemId: 'ecosystemsButton',
                margin: 10,
                width: '200px',
                text: 'Wetland Types'
            },
            {
                xtype: 'button',
                disabled: false,
                itemId: 'acknowButton',
                margin: 10,
                width: '200px',
                text: 'Acknowledgments'
            },
            {
                xtype: 'container',
                baseCls: 'coverTitle',
                docked: 'bottom',
                height: '80.px',
                layout: 'hbox',
                items: [
                    {
                        xtype: 'image',
                        docked: 'left',
                        height: '100%',
                        width: '33%',
                        src: 'resources/icons/CNHP.png'
                    },
                    {
                        xtype: 'image',
                        docked: 'left',
                        width: '33%',
                        src: 'resources/icons/EPA.gif'
                    },
                    {
                        xtype: 'image',
                        docked: 'left',
                        width: '33%',
                        src: 'resources/icons/CSU.png'
                    }
                ]
            }
        ]
    }

});