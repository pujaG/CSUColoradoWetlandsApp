/*
 * File: app/view/legendPanel.js
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

Ext.define('CWIC.view.legendPanel', {
    extend: 'Ext.Panel',
    alias: 'widget.legend',

    config: {
        centered: true,
        height: '80%',
        html: '<img src="resources/images/NWI_legend.png">',
        itemId: 'key',
        maxHeight: '245px',
        maxWidth: '155px',
        width: '80%',
        hideOnMaskTap: true,
        layout: 'card',
        modal: true,
        scrollable: 'vertical'
    }

});