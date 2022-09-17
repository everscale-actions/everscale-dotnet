/*
 * Copyright 2018-2020 TON Labs LTD.
 *
 * Licensed under the SOFTWARE EVALUATION License (the "License"); you may not use
 * this file except in compliance with the License.
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific TON DEV software governing permissions and
 * limitations under the License.
 *
 */

// This file is just a template that used to generate index.js at npm installation stage

const workerScript = `//****************************************************************** WRAPPER BEGIN

let wasm;

const cachedTextDecoder = new TextDecoder('utf-8', { ignoreBOM: true, fatal: true });

cachedTextDecoder.decode();

let cachedUint8Memory0 = new Uint8Array();

function getUint8Memory0() {
    if (cachedUint8Memory0.byteLength === 0) {
        cachedUint8Memory0 = new Uint8Array(wasm.memory.buffer);
    }
    return cachedUint8Memory0;
}

function getStringFromWasm0(ptr, len) {
    return cachedTextDecoder.decode(getUint8Memory0().subarray(ptr, ptr + len));
}

const heap = new Array(32).fill(undefined);

heap.push(undefined, null, true, false);

let heap_next = heap.length;

function addHeapObject(obj) {
    if (heap_next === heap.length) heap.push(heap.length + 1);
    const idx = heap_next;
    heap_next = heap[idx];

    heap[idx] = obj;
    return idx;
}

function getObject(idx) { return heap[idx]; }

function dropObject(idx) {
    if (idx < 36) return;
    heap[idx] = heap_next;
    heap_next = idx;
}

function takeObject(idx) {
    const ret = getObject(idx);
    dropObject(idx);
    return ret;
}

let WASM_VECTOR_LEN = 0;

const cachedTextEncoder = new TextEncoder('utf-8');

const encodeString = (typeof cachedTextEncoder.encodeInto === 'function'
    ? function (arg, view) {
    return cachedTextEncoder.encodeInto(arg, view);
}
    : function (arg, view) {
    const buf = cachedTextEncoder.encode(arg);
    view.set(buf);
    return {
        read: arg.length,
        written: buf.length
    };
});

function passStringToWasm0(arg, malloc, realloc) {

    if (realloc === undefined) {
        const buf = cachedTextEncoder.encode(arg);
        const ptr = malloc(buf.length);
        getUint8Memory0().subarray(ptr, ptr + buf.length).set(buf);
        WASM_VECTOR_LEN = buf.length;
        return ptr;
    }

    let len = arg.length;
    let ptr = malloc(len);

    const mem = getUint8Memory0();

    let offset = 0;

    for (; offset < len; offset++) {
        const code = arg.charCodeAt(offset);
        if (code > 0x7F) break;
        mem[ptr + offset] = code;
    }

    if (offset !== len) {
        if (offset !== 0) {
            arg = arg.slice(offset);
        }
        ptr = realloc(ptr, len, len = offset + arg.length * 3);
        const view = getUint8Memory0().subarray(ptr + offset, ptr + len);
        const ret = encodeString(arg, view);

        offset += ret.written;
    }

    WASM_VECTOR_LEN = offset;
    return ptr;
}

function isLikeNone(x) {
    return x === undefined || x === null;
}

let cachedInt32Memory0 = new Int32Array();

function getInt32Memory0() {
    if (cachedInt32Memory0.byteLength === 0) {
        cachedInt32Memory0 = new Int32Array(wasm.memory.buffer);
    }
    return cachedInt32Memory0;
}

let cachedFloat64Memory0 = new Float64Array();

function getFloat64Memory0() {
    if (cachedFloat64Memory0.byteLength === 0) {
        cachedFloat64Memory0 = new Float64Array(wasm.memory.buffer);
    }
    return cachedFloat64Memory0;
}

function debugString(val) {
    // primitive types
    const type = typeof val;
    if (type == 'number' || type == 'boolean' || val == null) {
        return  \`\${val}\`;
    }
    if (type == 'string') {
        return \`"\${val}"\`;
    }
    if (type == 'symbol') {
        const description = val.description;
        if (description == null) {
            return 'Symbol';
        } else {
            return \`Symbol(\${description})\`;
        }
    }
    if (type == 'function') {
        const name = val.name;
        if (typeof name == 'string' && name.length > 0) {
            return \`Function(\${name})\`;
        } else {
            return 'Function';
        }
    }
    // objects
    if (Array.isArray(val)) {
        const length = val.length;
        let debug = '[';
        if (length > 0) {
            debug += debugString(val[0]);
        }
        for(let i = 1; i < length; i++) {
            debug += ', ' + debugString(val[i]);
        }
        debug += ']';
        return debug;
    }
    // Test for built-in
    const builtInMatches = /\\[object ([^\\]]+)\\]/.exec(toString.call(val));
    let className;
    if (builtInMatches.length > 1) {
        className = builtInMatches[1];
    } else {
        // Failed to match the standard '[object ClassName]'
        return toString.call(val);
    }
    if (className == 'Object') {
        // we're a user defined class or Object
        // JSON.stringify avoids problems with cycles, and is generally much
        // easier than looping through ownProperties of \`val\`.
        try {
            return 'Object(' + JSON.stringify(val) + ')';
        } catch (_) {
            return 'Object';
        }
    }
    // errors
    if (val instanceof Error) {
        return \`\${val.name}: \${val.message}\\n\${val.stack}\`;
    }
    // TODO we could test for more things here, like \`Set\`s and \`Map\`s.
    return className;
}

function makeClosure(arg0, arg1, dtor, f) {
    const state = { a: arg0, b: arg1, cnt: 1, dtor };
    const real = (...args) => {
        // First up with a closure we increment the internal reference
        // count. This ensures that the Rust closure environment won't
        // be deallocated while we're invoking it.
        state.cnt++;
        try {
            return f(state.a, state.b, ...args);
        } finally {
            if (--state.cnt === 0) {
                wasm.__wbindgen_export_2.get(state.dtor)(state.a, state.b);
                state.a = 0;

            }
        }
    };
    real.original = state;

    return real;
}
function __wbg_adapter_36(arg0, arg1, arg2) {
    wasm._dyn_core__ops__function__Fn__A____Output___R_as_wasm_bindgen__closure__WasmClosure___describe__invoke__ha1a803b9412ac3ef(arg0, arg1, addHeapObject(arg2));
}

function __wbg_adapter_39(arg0, arg1) {
    wasm._dyn_core__ops__function__Fn_____Output___R_as_wasm_bindgen__closure__WasmClosure___describe__invoke__he5783d8ee1a38c13(arg0, arg1);
}

function makeMutClosure(arg0, arg1, dtor, f) {
    const state = { a: arg0, b: arg1, cnt: 1, dtor };
    const real = (...args) => {
        // First up with a closure we increment the internal reference
        // count. This ensures that the Rust closure environment won't
        // be deallocated while we're invoking it.
        state.cnt++;
        const a = state.a;
        state.a = 0;
        try {
            return f(a, state.b, ...args);
        } finally {
            if (--state.cnt === 0) {
                wasm.__wbindgen_export_2.get(state.dtor)(a, state.b);

            } else {
                state.a = a;
            }
        }
    };
    real.original = state;

    return real;
}
function __wbg_adapter_42(arg0, arg1, arg2) {
    try {
        const retptr = wasm.__wbindgen_add_to_stack_pointer(-16);
        wasm._dyn_core__ops__function__FnMut__A____Output___R_as_wasm_bindgen__closure__WasmClosure___describe__invoke__hb4f2a2c4b323ad31(retptr, arg0, arg1, addHeapObject(arg2));
        var r0 = getInt32Memory0()[retptr / 4 + 0];
        var r1 = getInt32Memory0()[retptr / 4 + 1];
        if (r1) {
            throw takeObject(r0);
        }
    } finally {
        wasm.__wbindgen_add_to_stack_pointer(16);
    }
}

function __wbg_adapter_45(arg0, arg1) {
    wasm._dyn_core__ops__function__FnMut_____Output___R_as_wasm_bindgen__closure__WasmClosure___describe__invoke__ha68f87c05c57114b(arg0, arg1);
}

function __wbg_adapter_48(arg0, arg1, arg2) {
    wasm._dyn_core__ops__function__FnMut__A____Output___R_as_wasm_bindgen__closure__WasmClosure___describe__invoke__h4adc58b2e7650f37(arg0, arg1, addHeapObject(arg2));
}

function __wbg_adapter_51(arg0, arg1, arg2) {
    wasm._dyn_core__ops__function__FnMut__A____Output___R_as_wasm_bindgen__closure__WasmClosure___describe__invoke__hdacc065b17a63d45(arg0, arg1, addHeapObject(arg2));
}

function __wbg_adapter_54(arg0, arg1, arg2) {
    wasm._dyn_core__ops__function__FnMut__A____Output___R_as_wasm_bindgen__closure__WasmClosure___describe__invoke__hf16e572565278138(arg0, arg1, addHeapObject(arg2));
}

/**
* @param {string} config_json
* @returns {string}
*/
function core_create_context(config_json) {
    try {
        const retptr = wasm.__wbindgen_add_to_stack_pointer(-16);
        const ptr0 = passStringToWasm0(config_json, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
        const len0 = WASM_VECTOR_LEN;
        wasm.core_create_context(retptr, ptr0, len0);
        var r0 = getInt32Memory0()[retptr / 4 + 0];
        var r1 = getInt32Memory0()[retptr / 4 + 1];
        return getStringFromWasm0(r0, r1);
    } finally {
        wasm.__wbindgen_add_to_stack_pointer(16);
        wasm.__wbindgen_free(r0, r1);
    }
}

/**
* @param {number} context
*/
function core_destroy_context(context) {
    wasm.core_destroy_context(context);
}

/**
* @param {number} context
* @param {string} function_name
* @param {any} params
* @param {number} request_id
*/
function core_request(context, function_name, params, request_id) {
    try {
        const retptr = wasm.__wbindgen_add_to_stack_pointer(-16);
        const ptr0 = passStringToWasm0(function_name, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
        const len0 = WASM_VECTOR_LEN;
        wasm.core_request(retptr, context, ptr0, len0, addHeapObject(params), request_id);
        var r0 = getInt32Memory0()[retptr / 4 + 0];
        var r1 = getInt32Memory0()[retptr / 4 + 1];
        if (r1) {
            throw takeObject(r0);
        }
    } finally {
        wasm.__wbindgen_add_to_stack_pointer(16);
    }
}

function handleError(f, args) {
    try {
        return f.apply(this, args);
    } catch (e) {
        wasm.__wbindgen_exn_store(addHeapObject(e));
    }
}

function getArrayU8FromWasm0(ptr, len) {
    return getUint8Memory0().subarray(ptr / 1, ptr / 1 + len);
}

async function load(module, imports) {
    if (typeof Response === 'function' && module instanceof Response) {
        if (typeof WebAssembly.instantiateStreaming === 'function') {
            try {
                return await WebAssembly.instantiateStreaming(module, imports);

            } catch (e) {
                if (module.headers.get('Content-Type') != 'application/wasm') {
                    console.warn("\`WebAssembly.instantiateStreaming\` failed because your server does not serve wasm with \`application/wasm\` MIME type. Falling back to \`WebAssembly.instantiate\` which is slower. Original error:\\n", e);

                } else {
                    throw e;
                }
            }
        }

        const bytes = await module.arrayBuffer();
        return await WebAssembly.instantiate(bytes, imports);

    } else {
        const instance = await WebAssembly.instantiate(module, imports);

        if (instance instanceof WebAssembly.Instance) {
            return { instance, module };

        } else {
            return instance;
        }
    }
}

function getImports() {
    const imports = {};
    imports.wbg = {};
    imports.wbg.__wbg_new_8d2af00bc1e329ee = function(arg0, arg1) {
        const ret = new Error(getStringFromWasm0(arg0, arg1));
        return addHeapObject(ret);
    };
    imports.wbg.__wbindgen_string_new = function(arg0, arg1) {
        const ret = getStringFromWasm0(arg0, arg1);
        return addHeapObject(ret);
    };
    imports.wbg.__wbindgen_memory = function() {
        const ret = wasm.memory;
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_buffer_3f3d764d4747d564 = function(arg0) {
        const ret = getObject(arg0).buffer;
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_newwithbyteoffsetandlength_d9aa266703cb98be = function(arg0, arg1, arg2) {
        const ret = new Uint8Array(getObject(arg0), arg1 >>> 0, arg2 >>> 0);
        return addHeapObject(ret);
    };
    imports.wbg.__wbindgen_object_drop_ref = function(arg0) {
        takeObject(arg0);
    };
    imports.wbg.__wbg_newwithlength_7c42f7e738a9d5d3 = function(arg0) {
        const ret = new Array(arg0 >>> 0);
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_set_a68214f35c417fa9 = function(arg0, arg1, arg2) {
        getObject(arg0)[arg1 >>> 0] = takeObject(arg2);
    };
    imports.wbg.__wbg_newwithu8arraysequence_f863246af83e1785 = function() { return handleError(function (arg0) {
        const ret = new Blob(getObject(arg0));
        return addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_get_765201544a2b6869 = function() { return handleError(function (arg0, arg1) {
        const ret = Reflect.get(getObject(arg0), getObject(arg1));
        return addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_instanceof_ArrayBuffer_e5e48f4762c5610b = function(arg0) {
        let result;
        try {
            result = getObject(arg0) instanceof ArrayBuffer;
        } catch {
            result = false;
        }
        const ret = result;
        return ret;
    };
    imports.wbg.__wbindgen_is_object = function(arg0) {
        const val = getObject(arg0);
        const ret = typeof(val) === 'object' && val !== null;
        return ret;
    };
    imports.wbg.__wbg_new_8c3f0052272a457a = function(arg0) {
        const ret = new Uint8Array(getObject(arg0));
        return addHeapObject(ret);
    };
    imports.wbg.__wbindgen_object_clone_ref = function(arg0) {
        const ret = getObject(arg0);
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_keys_0702294afaeb6044 = function(arg0) {
        const ret = Object.keys(getObject(arg0));
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_length_6e3bbe7c8bd4dbd8 = function(arg0) {
        const ret = getObject(arg0).length;
        return ret;
    };
    imports.wbg.__wbg_get_57245cc7d7c7619d = function(arg0, arg1) {
        const ret = getObject(arg0)[arg1 >>> 0];
        return addHeapObject(ret);
    };
    imports.wbg.__wbindgen_string_get = function(arg0, arg1) {
        const obj = getObject(arg1);
        const ret = typeof(obj) === 'string' ? obj : undefined;
        var ptr0 = isLikeNone(ret) ? 0 : passStringToWasm0(ret, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
        var len0 = WASM_VECTOR_LEN;
        getInt32Memory0()[arg0 / 4 + 1] = len0;
        getInt32Memory0()[arg0 / 4 + 0] = ptr0;
    };
    imports.wbg.__wbg_new0_a57059d72c5b7aee = function() {
        const ret = new Date();
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_getTime_cb82adb2556ed13e = function(arg0) {
        const ret = getObject(arg0).getTime();
        return ret;
    };
    imports.wbg.__wbindgen_boolean_get = function(arg0) {
        const v = getObject(arg0);
        const ret = typeof(v) === 'boolean' ? (v ? 1 : 0) : 2;
        return ret;
    };
    imports.wbg.__wbindgen_is_bigint = function(arg0) {
        const ret = typeof(getObject(arg0)) === 'bigint';
        return ret;
    };
    imports.wbg.__wbg_BigInt_06819bca5a5bedef = function(arg0) {
        const ret = BigInt(getObject(arg0));
        return ret;
    };
    imports.wbg.__wbg_BigInt_d0c7d465bfa30d3b = function(arg0) {
        const ret = BigInt(arg0);
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_is_40a66842732708e7 = function(arg0, arg1) {
        const ret = Object.is(getObject(arg0), getObject(arg1));
        return ret;
    };
    imports.wbg.__wbindgen_number_get = function(arg0, arg1) {
        const obj = getObject(arg1);
        const ret = typeof(obj) === 'number' ? obj : undefined;
        getFloat64Memory0()[arg0 / 8 + 1] = isLikeNone(ret) ? 0 : ret;
        getInt32Memory0()[arg0 / 4 + 0] = !isLikeNone(ret);
    };
    imports.wbg.__wbg_isSafeInteger_dfa0593e8d7ac35a = function(arg0) {
        const ret = Number.isSafeInteger(getObject(arg0));
        return ret;
    };
    imports.wbg.__wbg_isArray_27c46c67f498e15d = function(arg0) {
        const ret = Array.isArray(getObject(arg0));
        return ret;
    };
    imports.wbg.__wbg_iterator_6f9d4f28845f426c = function() {
        const ret = Symbol.iterator;
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_BigInt_67359e71cae1c6c9 = function(arg0) {
        const ret = BigInt(getObject(arg0));
        return ret;
    };
    imports.wbg.__wbg_BigInt_1fab4952b6c4a499 = function(arg0) {
        const ret = BigInt(BigInt.asUintN(64, arg0));
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_entries_65a76a413fc91037 = function(arg0) {
        const ret = Object.entries(getObject(arg0));
        return addHeapObject(ret);
    };
    imports.wbg.__wbindgen_is_undefined = function(arg0) {
        const ret = getObject(arg0) === undefined;
        return ret;
    };
    imports.wbg.__wbindgen_is_null = function(arg0) {
        const ret = getObject(arg0) === null;
        return ret;
    };
    imports.wbg.__wbg_String_c9c0f9be374874ba = function(arg0, arg1) {
        const ret = String(getObject(arg1));
        const ptr0 = passStringToWasm0(ret, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
        const len0 = WASM_VECTOR_LEN;
        getInt32Memory0()[arg0 / 4 + 1] = len0;
        getInt32Memory0()[arg0 / 4 + 0] = ptr0;
    };
    imports.wbg.__wbindgen_number_new = function(arg0) {
        const ret = arg0;
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_new_0b9bfdd97583284e = function() {
        const ret = new Object();
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_String_33099856e8a8246a = function(arg0, arg1) {
        const ret = String(getObject(arg1));
        const ptr0 = passStringToWasm0(ret, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
        const len0 = WASM_VECTOR_LEN;
        getInt32Memory0()[arg0 / 4 + 1] = len0;
        getInt32Memory0()[arg0 / 4 + 0] = ptr0;
    };
    imports.wbg.__wbg_coreresponsehandler_a52879454c60c764 = function(arg0, arg1, arg2, arg3) {
        core_response_handler(arg0 >>> 0, takeObject(arg1), arg2 >>> 0, arg3 !== 0);
    };
    imports.wbg.__wbg_message_fe2af63ccc8985bc = function(arg0) {
        const ret = getObject(arg0).message;
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_new_1d9a920c6bfc44a8 = function() {
        const ret = new Array();
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_new_268f7b7dd3430798 = function() {
        const ret = new Map();
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_set_933729cf5b66ac11 = function(arg0, arg1, arg2) {
        const ret = getObject(arg0).set(getObject(arg1), getObject(arg2));
        return addHeapObject(ret);
    };
    imports.wbg.__wbindgen_is_string = function(arg0) {
        const ret = typeof(getObject(arg0)) === 'string';
        return ret;
    };
    imports.wbg.__wbg_set_fbc33d020f507b72 = function(arg0, arg1, arg2) {
        getObject(arg0)[takeObject(arg1)] = takeObject(arg2);
    };
    imports.wbg.__wbg_process_0cc2ada8524d6f83 = function(arg0) {
        const ret = getObject(arg0).process;
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_versions_c11acceab27a6c87 = function(arg0) {
        const ret = getObject(arg0).versions;
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_node_7ff1ce49caf23815 = function(arg0) {
        const ret = getObject(arg0).node;
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_static_accessor_NODE_MODULE_cf6401cc1091279e = function() {
        const ret = module;
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_require_a746e79b322b9336 = function() { return handleError(function (arg0, arg1, arg2) {
        const ret = getObject(arg0).require(getStringFromWasm0(arg1, arg2));
        return addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_crypto_2036bed7c44c25e7 = function(arg0) {
        const ret = getObject(arg0).crypto;
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_msCrypto_a21fc88caf1ecdc8 = function(arg0) {
        const ret = getObject(arg0).msCrypto;
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_newwithlength_f5933855e4f48a19 = function(arg0) {
        const ret = new Uint8Array(arg0 >>> 0);
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_transaction_cce96cbebd81fe1c = function() { return handleError(function (arg0, arg1, arg2, arg3) {
        const ret = getObject(arg0).transaction(getStringFromWasm0(arg1, arg2), takeObject(arg3));
        return addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_setoncomplete_3e57a8cec8327f66 = function(arg0, arg1) {
        getObject(arg0).oncomplete = getObject(arg1);
    };
    imports.wbg.__wbg_setonerror_00051c0213f27b2c = function(arg0, arg1) {
        getObject(arg0).onerror = getObject(arg1);
    };
    imports.wbg.__wbg_setonabort_404bee3b9940d03d = function(arg0, arg1) {
        getObject(arg0).onabort = getObject(arg1);
    };
    imports.wbg.__wbg_item_52a6bec36314687b = function(arg0, arg1, arg2) {
        const ret = getObject(arg1).item(arg2 >>> 0);
        var ptr0 = isLikeNone(ret) ? 0 : passStringToWasm0(ret, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
        var len0 = WASM_VECTOR_LEN;
        getInt32Memory0()[arg0 / 4 + 1] = len0;
        getInt32Memory0()[arg0 / 4 + 0] = ptr0;
    };
    imports.wbg.__wbg_objectStore_f17976b0e6377830 = function() { return handleError(function (arg0, arg1, arg2) {
        const ret = getObject(arg0).objectStore(getStringFromWasm0(arg1, arg2));
        return addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_target_bf704b7db7ad1387 = function(arg0) {
        const ret = getObject(arg0).target;
        return isLikeNone(ret) ? 0 : addHeapObject(ret);
    };
    imports.wbg.__wbg_readyState_fb287f170113917c = function(arg0) {
        const ret = getObject(arg0).readyState;
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_setonsuccess_5f71593bc51653a3 = function(arg0, arg1) {
        getObject(arg0).onsuccess = getObject(arg1);
    };
    imports.wbg.__wbg_setonerror_d5771cc5bf9ea74c = function(arg0, arg1) {
        getObject(arg0).onerror = getObject(arg1);
    };
    imports.wbg.__wbindgen_cb_drop = function(arg0) {
        const obj = takeObject(arg0).original;
        if (obj.cnt-- == 1) {
            obj.a = 0;
            return true;
        }
        const ret = false;
        return ret;
    };
    imports.wbg.__wbg_next_aaef7c8aa5e212ac = function() { return handleError(function (arg0) {
        const ret = getObject(arg0).next();
        return addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_done_1b73b0672e15f234 = function(arg0) {
        const ret = getObject(arg0).done;
        return ret;
    };
    imports.wbg.__wbg_value_1ccc36bc03462d71 = function(arg0) {
        const ret = getObject(arg0).value;
        return addHeapObject(ret);
    };
    imports.wbg.__wbindgen_is_function = function(arg0) {
        const ret = typeof(getObject(arg0)) === 'function';
        return ret;
    };
    imports.wbg.__wbg_call_97ae9d8645dc388b = function() { return handleError(function (arg0, arg1) {
        const ret = getObject(arg0).call(getObject(arg1));
        return addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_next_579e583d33566a86 = function(arg0) {
        const ret = getObject(arg0).next;
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_self_6d479506f72c6a71 = function() { return handleError(function () {
        const ret = self.self;
        return addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_window_f2557cc78490aceb = function() { return handleError(function () {
        const ret = window.window;
        return addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_globalThis_7f206bda628d5286 = function() { return handleError(function () {
        const ret = globalThis.globalThis;
        return addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_global_ba75c50d1cf384f4 = function() { return handleError(function () {
        const ret = global.global;
        return addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_newnoargs_b5b063fc6c2f0376 = function(arg0, arg1) {
        const ret = new Function(getStringFromWasm0(arg0, arg1));
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_set_83db9690f9353e79 = function(arg0, arg1, arg2) {
        getObject(arg0).set(getObject(arg1), arg2 >>> 0);
    };
    imports.wbg.__wbg_length_9e1ae1900cb0fbd5 = function(arg0) {
        const ret = getObject(arg0).length;
        return ret;
    };
    imports.wbg.__wbg_has_8359f114ce042f5a = function() { return handleError(function (arg0, arg1) {
        const ret = Reflect.has(getObject(arg0), getObject(arg1));
        return ret;
    }, arguments) };
    imports.wbg.__wbg_set_bf3f89b92d5a34bf = function() { return handleError(function (arg0, arg1, arg2) {
        const ret = Reflect.set(getObject(arg0), getObject(arg1), getObject(arg2));
        return ret;
    }, arguments) };
    imports.wbg.__wbg_randomFillSync_065afffde01daa66 = function() { return handleError(function (arg0, arg1, arg2) {
        getObject(arg0).randomFillSync(getArrayU8FromWasm0(arg1, arg2));
    }, arguments) };
    imports.wbg.__wbg_subarray_58ad4efbb5bcb886 = function(arg0, arg1, arg2) {
        const ret = getObject(arg0).subarray(arg1 >>> 0, arg2 >>> 0);
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_getRandomValues_b99eec4244a475bb = function() { return handleError(function (arg0, arg1) {
        getObject(arg0).getRandomValues(getObject(arg1));
    }, arguments) };
    imports.wbg.__wbg_self_7eede1f4488bf346 = function() { return handleError(function () {
        const ret = self.self;
        return addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_crypto_c909fb428dcbddb6 = function(arg0) {
        const ret = getObject(arg0).crypto;
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_msCrypto_511eefefbfc70ae4 = function(arg0) {
        const ret = getObject(arg0).msCrypto;
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_static_accessor_MODULE_ef3aa2eb251158a5 = function() {
        const ret = module;
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_require_900d5c3984fe7703 = function(arg0, arg1, arg2) {
        const ret = getObject(arg0).require(getStringFromWasm0(arg1, arg2));
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_getRandomValues_307049345d0bd88c = function(arg0) {
        const ret = getObject(arg0).getRandomValues;
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_randomFillSync_85b3f4c52c56c313 = function(arg0, arg1, arg2) {
        getObject(arg0).randomFillSync(getArrayU8FromWasm0(arg1, arg2));
    };
    imports.wbg.__wbg_getRandomValues_cd175915511f705e = function(arg0, arg1) {
        getObject(arg0).getRandomValues(getObject(arg1));
    };
    imports.wbg.__wbg_instanceof_Uint8Array_971eeda69eb75003 = function(arg0) {
        let result;
        try {
            result = getObject(arg0) instanceof Uint8Array;
        } catch {
            result = false;
        }
        const ret = result;
        return ret;
    };
    imports.wbg.__wbg_put_84e7fc93eee27b28 = function() { return handleError(function (arg0, arg1, arg2) {
        const ret = getObject(arg0).put(getObject(arg1), getObject(arg2));
        return addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_delete_8abedd1043b4105d = function() { return handleError(function (arg0, arg1) {
        const ret = getObject(arg0).delete(getObject(arg1));
        return addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_setonversionchange_840d65cd0888dfb0 = function(arg0, arg1) {
        getObject(arg0).onversionchange = getObject(arg1);
    };
    imports.wbg.__wbg_setonupgradeneeded_17d0b9530f1e0cac = function(arg0, arg1) {
        getObject(arg0).onupgradeneeded = getObject(arg1);
    };
    imports.wbg.__wbg_setonblocked_e66d6be5c879980d = function(arg0, arg1) {
        getObject(arg0).onblocked = getObject(arg1);
    };
    imports.wbg.__wbg_message_a7af3ee0cc0fe28d = function(arg0, arg1) {
        const ret = getObject(arg1).message;
        const ptr0 = passStringToWasm0(ret, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
        const len0 = WASM_VECTOR_LEN;
        getInt32Memory0()[arg0 / 4 + 1] = len0;
        getInt32Memory0()[arg0 / 4 + 0] = ptr0;
    };
    imports.wbg.__wbg_Window_5684341ff6dfe3ad = function(arg0) {
        const ret = getObject(arg0).Window;
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_WorkerGlobalScope_e0447ffcae8bb272 = function(arg0) {
        const ret = getObject(arg0).WorkerGlobalScope;
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_indexedDB_050f0962ab607ac5 = function() { return handleError(function (arg0) {
        const ret = getObject(arg0).indexedDB;
        return isLikeNone(ret) ? 0 : addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_indexedDB_8d9e9ab4616df7f0 = function() { return handleError(function (arg0) {
        const ret = getObject(arg0).indexedDB;
        return isLikeNone(ret) ? 0 : addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_open_a31c3fe1fdc244eb = function() { return handleError(function (arg0, arg1, arg2) {
        const ret = getObject(arg0).open(getStringFromWasm0(arg1, arg2));
        return addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_setTimeout_d6fcf0d9067b8e64 = function() { return handleError(function (arg0, arg1, arg2) {
        const ret = getObject(arg0).setTimeout(getObject(arg1), arg2);
        return ret;
    }, arguments) };
    imports.wbg.__wbg_clearTimeout_7d6f7bfeed34b348 = function(arg0, arg1) {
        getObject(arg0).clearTimeout(arg1);
    };
    imports.wbg.__wbg_newwithstrandinit_05d7180788420c40 = function() { return handleError(function (arg0, arg1, arg2) {
        const ret = new Request(getStringFromWasm0(arg0, arg1), getObject(arg2));
        return addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_headers_85824e993aa739bf = function(arg0) {
        const ret = getObject(arg0).headers;
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_set_992c1d31586b2957 = function() { return handleError(function (arg0, arg1, arg2, arg3, arg4) {
        getObject(arg0).set(getStringFromWasm0(arg1, arg2), getStringFromWasm0(arg3, arg4));
    }, arguments) };
    imports.wbg.__wbg_fetch_0fe04905cccfc2aa = function(arg0, arg1) {
        const ret = getObject(arg0).fetch(getObject(arg1));
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_instanceof_Response_eaa426220848a39e = function(arg0) {
        let result;
        try {
            result = getObject(arg0) instanceof Response;
        } catch {
            result = false;
        }
        const ret = result;
        return ret;
    };
    imports.wbg.__wbg_status_c4ef3dd591e63435 = function(arg0) {
        const ret = getObject(arg0).status;
        return ret;
    };
    imports.wbg.__wbg_url_74285ddf2747cb3d = function(arg0, arg1) {
        const ret = getObject(arg1).url;
        const ptr0 = passStringToWasm0(ret, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
        const len0 = WASM_VECTOR_LEN;
        getInt32Memory0()[arg0 / 4 + 1] = len0;
        getInt32Memory0()[arg0 / 4 + 0] = ptr0;
    };
    imports.wbg.__wbg_newwithstr_d5b5f9b985ee84fb = function() { return handleError(function (arg0, arg1, arg2, arg3) {
        const ret = new WebSocket(getStringFromWasm0(arg0, arg1), getStringFromWasm0(arg2, arg3));
        return addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_new_d29e507f6606de91 = function() { return handleError(function (arg0, arg1) {
        const ret = new WebSocket(getStringFromWasm0(arg0, arg1));
        return addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_setonmessage_c5a806b62a0c5607 = function(arg0, arg1) {
        getObject(arg0).onmessage = getObject(arg1);
    };
    imports.wbg.__wbg_setonopen_9ce48dce57e549b5 = function(arg0, arg1) {
        getObject(arg0).onopen = getObject(arg1);
    };
    imports.wbg.__wbg_setonerror_02393260b3e29972 = function(arg0, arg1) {
        getObject(arg0).onerror = getObject(arg1);
    };
    imports.wbg.__wbg_send_80b256d87a6779e5 = function() { return handleError(function (arg0, arg1, arg2) {
        getObject(arg0).send(getStringFromWasm0(arg1, arg2));
    }, arguments) };
    imports.wbg.__wbg_data_7b1f01f4e6a64fbe = function(arg0) {
        const ret = getObject(arg0).data;
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_stringify_d6471d300ded9b68 = function() { return handleError(function (arg0) {
        const ret = JSON.stringify(getObject(arg0));
        return addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_objectStoreNames_8c06c40d2b05141c = function(arg0) {
        const ret = getObject(arg0).objectStoreNames;
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_createObjectStore_d3e2789c13dde1fc = function() { return handleError(function (arg0, arg1, arg2) {
        const ret = getObject(arg0).createObjectStore(getStringFromWasm0(arg1, arg2));
        return addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_instanceof_Error_56b496a10a56de66 = function(arg0) {
        let result;
        try {
            result = getObject(arg0) instanceof Error;
        } catch {
            result = false;
        }
        const ret = result;
        return ret;
    };
    imports.wbg.__wbg_getTimezoneOffset_89bd4275e1ca8341 = function(arg0) {
        const ret = getObject(arg0).getTimezoneOffset();
        return ret;
    };
    imports.wbg.__wbindgen_debug_string = function(arg0, arg1) {
        const ret = debugString(getObject(arg1));
        const ptr0 = passStringToWasm0(ret, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
        const len0 = WASM_VECTOR_LEN;
        getInt32Memory0()[arg0 / 4 + 1] = len0;
        getInt32Memory0()[arg0 / 4 + 0] = ptr0;
    };
    imports.wbg.__wbindgen_throw = function(arg0, arg1) {
        throw new Error(getStringFromWasm0(arg0, arg1));
    };
    imports.wbg.__wbg_then_cedad20fbbd9418a = function(arg0, arg1, arg2) {
        const ret = getObject(arg0).then(getObject(arg1), getObject(arg2));
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_resolve_99fe17964f31ffc0 = function(arg0) {
        const ret = Promise.resolve(getObject(arg0));
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_then_11f7a54d67b4bfad = function(arg0, arg1) {
        const ret = getObject(arg0).then(getObject(arg1));
        return addHeapObject(ret);
    };
    imports.wbg.__wbg_result_9e399c14676970d9 = function() { return handleError(function (arg0) {
        const ret = getObject(arg0).result;
        return addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_error_aacf5ac191e54ed0 = function() { return handleError(function (arg0) {
        const ret = getObject(arg0).error;
        return isLikeNone(ret) ? 0 : addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_get_6285bf458a1ee758 = function() { return handleError(function (arg0, arg1) {
        const ret = getObject(arg0).get(getObject(arg1));
        return addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbg_instanceof_Window_acc97ff9f5d2c7b4 = function(arg0) {
        let result;
        try {
            result = true;
        } catch {
            result = false;
        }
        const ret = result;
        return ret;
    };
    imports.wbg.__wbg_close_45d053bea59e7746 = function() { return handleError(function (arg0) {
        getObject(arg0).close();
    }, arguments) };
    imports.wbg.__wbg_text_1169d752cc697903 = function() { return handleError(function (arg0) {
        const ret = getObject(arg0).text();
        return addHeapObject(ret);
    }, arguments) };
    imports.wbg.__wbindgen_closure_wrapper943 = function(arg0, arg1, arg2) {
        const ret = makeClosure(arg0, arg1, 46, __wbg_adapter_36);
        return addHeapObject(ret);
    };
    imports.wbg.__wbindgen_closure_wrapper955 = function(arg0, arg1, arg2) {
        const ret = makeClosure(arg0, arg1, 43, __wbg_adapter_39);
        return addHeapObject(ret);
    };
    imports.wbg.__wbindgen_closure_wrapper6218 = function(arg0, arg1, arg2) {
        const ret = makeMutClosure(arg0, arg1, 243, __wbg_adapter_42);
        return addHeapObject(ret);
    };
    imports.wbg.__wbindgen_closure_wrapper6286 = function(arg0, arg1, arg2) {
        const ret = makeMutClosure(arg0, arg1, 237, __wbg_adapter_45);
        return addHeapObject(ret);
    };
    imports.wbg.__wbindgen_closure_wrapper6765 = function(arg0, arg1, arg2) {
        const ret = makeMutClosure(arg0, arg1, 246, __wbg_adapter_48);
        return addHeapObject(ret);
    };
    imports.wbg.__wbindgen_closure_wrapper6766 = function(arg0, arg1, arg2) {
        const ret = makeMutClosure(arg0, arg1, 1206, __wbg_adapter_51);
        return addHeapObject(ret);
    };
    imports.wbg.__wbindgen_closure_wrapper6767 = function(arg0, arg1, arg2) {
        const ret = makeMutClosure(arg0, arg1, 240, __wbg_adapter_54);
        return addHeapObject(ret);
    };

    return imports;
}

function initMemory(imports, maybe_memory) {

}

function finalizeInit(instance, module) {
    wasm = instance.exports;
    init.__wbindgen_wasm_module = module;
    cachedFloat64Memory0 = new Float64Array();
    cachedInt32Memory0 = new Int32Array();
    cachedUint8Memory0 = new Uint8Array();


    return wasm;
}

function initSync(module) {
    const imports = getImports();

    initMemory(imports);

    if (!(module instanceof WebAssembly.Module)) {
        module = new WebAssembly.Module(module);
    }

    const instance = new WebAssembly.Instance(module, imports);

    return finalizeInit(instance, module);
}

async function init(input) {
    if (typeof input === 'undefined') {    }
    const imports = getImports();

    if (typeof input === 'string' || (typeof Request === 'function' && input instanceof Request) || (typeof URL === 'function' && input instanceof URL)) {
        input = fetch(input);
    }

    initMemory(imports);

    const { instance, module } = await load(await input, imports);

    return finalizeInit(instance, module);
}


//****************************************************************** WRAPPER END

function replaceUndefinedWithNulls(value) {
    if (value === undefined) {
        return null;
    }
    if (value instanceof Blob) {
        return value;
    }
    if (typeof value === "object" && value !== null) {
        const result = Array.isArray(value) ? [] : {};
        for (const key in value) {
            result[key] = replaceUndefinedWithNulls(value[key]);
        }
        return result;
    }
    return value;
};

function core_response_handler(request_id, params, response_type, finished) {
    postMessage({
        type: 'response',
        requestId: request_id,
        params: replaceUndefinedWithNulls(params),
        responseType: response_type,
        finished,
    });
}

async function replaceBlobsWithArrayBuffers(value) {
    if (value instanceof Blob) {
        return await value.arrayBuffer();
    }
    if (typeof value === "bigint") {
        if (value < Number.MAX_SAFE_INTEGER && value > Number.MIN_SAFE_INTEGER) {
            return Number(value);
        } else {
            return value.toString();
        }
    }
    if (typeof value === "object" && value !== null) {
        const result = Array.isArray(value) ? [] : {};
        for (const key in value) {
            result[key] = await replaceBlobsWithArrayBuffers(value[key]);
        }
        return result;
    }
    return value;
}

self.onmessage = (e) => {
    const message = e.data;
    switch (message.type) {
    case 'init':
        (async () => {
            await init(message.wasmModule);
            postMessage({ type: 'init' });
        })();
        break;

    case 'createContext':
        postMessage({
            type: 'createContext',
            result: core_create_context(message.configJson),
            requestId: message.requestId,
        });
        break;

    case 'destroyContext':
        core_destroy_context(message.context);
        postMessage({
            type: 'destroyContext'
        });
        break;

    case 'request':
        (async () => {
            core_request(
                message.context,
                message.functionName,
                await replaceBlobsWithArrayBuffers(message.functionParams),
                message.requestId,
            );
        })();
        break;
    }
};
`;

let options = null;

export function libWebSetup(libOptions) {
    options = libOptions;
}

function debugLog(message) {
    if (options && options.debugLog) {
        options.debugLog(message);
    }
}


async function loadModule() {
    const startLoadTime = Date.now();
    let wasmModule;
    if (options && options.loadModule) {
        wasmModule = await options.loadModule;
    } else {
        const fetched = fetch((options && options.binaryURL) || "/eversdk.wasm");
        if (WebAssembly.compileStreaming) {
            debugLog("compileStreaming binary");
            return await WebAssembly.compileStreaming(fetched);
        }
        debugLog("compile binary");
        wasmModule = await WebAssembly.compile(await (await fetched).arrayBuffer());
    }
    await init(wasmModule);
    debugLog(`compile time ${Date.now() - startLoadTime}`);
}

function withSeparateWorker() {
    function debugLog(message) {
        if (options && options.debugLog) {
            options.debugLog(message);
        }
    }

    const workerBlob = new Blob(
        [workerScript],
        { type: "application/javascript" },
    );
    const workerUrl = URL.createObjectURL(workerBlob);
    const worker = new Worker(workerUrl);


    let nextCreateContextRequestId = 1;
    const createContextRequests = new Map();
    let initComplete = false;

    let responseHandler = null;

    worker.onmessage = (evt) => {
        const message = evt.data;
        switch (message.type) {
        case "init":
            initComplete = true;
            for (const [requestId, request] of createContextRequests.entries()) {
                worker.postMessage({
                    type: "createContext",
                    requestId,
                    configJson: request.configJson,
                });
            }
            break;
        case "createContext":
            const request = createContextRequests.get(message.requestId);
            if (request) {
                createContextRequests.delete(message.requestId);
                request.resolve(message.result);
            }
            break;
        case "destroyContext":
            break;
        case "response":
            if (responseHandler) {
                responseHandler(
                    message.requestId,
                    message.params,
                    message.responseType,
                    message.finished,
                );
            }
            break;
        }
    };

    worker.onerror = (evt) => {
        console.log(`Error from Web Worker: ${evt.message}`);
    };

    (async () => {
        worker.postMessage({
            type: "init",
            wasmModule: await loadModule(),
        });
    })();

    return Promise.resolve({
        setResponseParamsHandler: (handler) => {
            responseHandler = handler;
        },
        createContext: (configJson) => {
            return new Promise((resolve) => {
                const requestId = nextCreateContextRequestId;
                nextCreateContextRequestId += 1;
                createContextRequests.set(requestId, {
                    configJson,
                    resolve,
                });
                if (initComplete) {
                    worker.postMessage({
                        type: "createContext",
                        requestId,
                        configJson,
                    });
                }
            });
        },
        destroyContext: (context) => {
            worker.postMessage({
                type: "destroyContext",
                context,
            });
        },
        sendRequestParams: (context, requestId, functionName, functionParams) => {
            worker.postMessage({
                type: "request",
                context,
                requestId,
                functionName,
                functionParams,
            });
        },
    });
}

function withoutSeparateWorker() {
//****************************************************************** WRAPPER BEGIN

let wasm;

const cachedTextDecoder = new TextDecoder('utf-8', { ignoreBOM: true, fatal: true });

cachedTextDecoder.decode();

let cachedUint8Memory0 = new Uint8Array();

function getUint8Memory0() {
    if (cachedUint8Memory0.byteLength === 0) {
        cachedUint8Memory0 = new Uint8Array(wasm.memory.buffer);
    }
    return cachedUint8Memory0;
}

function getStringFromWasm0(ptr, len) {
    return cachedTextDecoder.decode(getUint8Memory0().subarray(ptr, ptr + len));
}

const heap = new Array(32).fill(undefined);

heap.push(undefined, null, true, false);

let heap_next = heap.length;

function addHeapObject(obj) {
    if (heap_next === heap.length) heap.push(heap.length + 1);
    const idx = heap_next;
    heap_next = heap[idx];

    heap[idx] = obj;
    return idx;
}

function getObject(idx) { return heap[idx]; }

function dropObject(idx) {
    if (idx < 36) return;
    heap[idx] = heap_next;
    heap_next = idx;
}

function takeObject(idx) {
    const ret = getObject(idx);
    dropObject(idx);
    return ret;
}

let WASM_VECTOR_LEN = 0;

const cachedTextEncoder = new TextEncoder('utf-8');

const encodeString = (typeof cachedTextEncoder.encodeInto === 'function'
    ? function (arg, view) {
    return cachedTextEncoder.encodeInto(arg, view);
}
    : function (arg, view) {
    const buf = cachedTextEncoder.encode(arg);
    view.set(buf);
    return {
        read: arg.length,
        written: buf.length
    };
});

function passStringToWasm0(arg, malloc, realloc) {

    if (realloc === undefined) {
        const buf = cachedTextEncoder.encode(arg);
        const ptr = malloc(buf.length);
        getUint8Memory0().subarray(ptr, ptr + buf.length).set(buf);
        WASM_VECTOR_LEN = buf.length;
        return ptr;
    }

    let len = arg.length;
    let ptr = malloc(len);

    const mem = getUint8Memory0();

    let offset = 0;

    for (; offset < len; offset++) {
        const code = arg.charCodeAt(offset);
        if (code > 0x7F) break;
        mem[ptr + offset] = code;
    }

    if (offset !== len) {
        if (offset !== 0) {
            arg = arg.slice(offset);
        }
        ptr = realloc(ptr, len, len = offset + arg.length * 3);
        const view = getUint8Memory0().subarray(ptr + offset, ptr + len);
        const ret = encodeString(arg, view);

        offset += ret.written;
    }

    WASM_VECTOR_LEN = offset;
    return ptr;
}

function isLikeNone(x) {
    return x === undefined || x === null;
}

let cachedInt32Memory0 = new Int32Array();

function getInt32Memory0() {
    if (cachedInt32Memory0.byteLength === 0) {
        cachedInt32Memory0 = new Int32Array(wasm.memory.buffer);
    }
    return cachedInt32Memory0;
}

let cachedFloat64Memory0 = new Float64Array();

function getFloat64Memory0() {
    if (cachedFloat64Memory0.byteLength === 0) {
        cachedFloat64Memory0 = new Float64Array(wasm.memory.buffer);
    }
    return cachedFloat64Memory0;
}

function debugString(val) {
    // primitive types
    const type = typeof val;
    if (type == 'number' || type == 'boolean' || val == null) {
        return  `${val}`;
    }
    if (type == 'string') {
        return `"${val}"`;
    }
    if (type == 'symbol') {
        const description = val.description;
        if (description == null) {
            return 'Symbol';
        } else {
            return `Symbol(${description})`;
        }
    }
    if (type == 'function') {
        const name = val.name;
        if (typeof name == 'string' && name.length > 0) {
            return `Function(${name})`;
        } else {
            return 'Function';
        }
    }
    // objects
    if (Array.isArray(val)) {
        const length = val.length;
        let debug = '[';
        if (length > 0) {
            debug += debugString(val[0]);
        }
        for(let i = 1; i < length; i++) {
            debug += ', ' + debugString(val[i]);
        }
        debug += ']';
        return debug;
    }
    // Test for built-in
    const builtInMatches = /\[object ([^\]]+)\]/.exec(toString.call(val));
    let className;
    if (builtInMatches.length > 1) {
        className = builtInMatches[1];
    } else {
        // Failed to match the standard '[object ClassName]'
        return toString.call(val);
    }
    if (className == 'Object') {
        // we're a user defined class or Object
        // JSON.stringify avoids problems with cycles, and is generally much
        // easier than looping through ownProperties of `val`.
        try {
            return 'Object(' + JSON.stringify(val) + ')';
        } catch (_) {
            return 'Object';
        }
    }
    // errors
    if (val instanceof Error) {
        return `${val.name}: ${val.message}\n${val.stack}`;
    }
    // TODO we could test for more things here, like `Set`s and `Map`s.
    return className;
}

function makeClosure(arg0, arg1, dtor, f) {
    const state = { a: arg0, b: arg1, cnt: 1, dtor };
    const real = (...args) => {
        // First up with a closure we increment the internal reference
        // count. This ensures that the Rust closure environment won't
        // be deallocated while we're invoking it.
        state.cnt++;
        try {
            return f(state.a, state.b, ...args);
        } finally {
            if (--state.cnt === 0) {
                wasm.__wbindgen_export_2.get(state.dtor)(state.a, state.b);
                state.a = 0;

            }
        }
    };
    real.original = state;

    return real;
}
function __wbg_adapter_36(arg0, arg1, arg2) {
    wasm._dyn_core__ops__function__Fn__A____Output___R_as_wasm_bindgen__closure__WasmClosure___describe__invoke__ha1a803b9412ac3ef(arg0, arg1, addHeapObject(arg2));
}

function __wbg_adapter_39(arg0, arg1) {
  wasm._dyn_core__ops__function__Fn_____Output___R_as_wasm_bindgen__closure__WasmClosure___describe__invoke__he5783d8ee1a38c13(arg0, arg1);
}

function makeMutClosure(arg0, arg1, dtor, f) {
    const state = { a: arg0, b: arg1, cnt: 1, dtor };
    const real = (...args) => {
        // First up with a closure we increment the internal reference
        // count. This ensures that the Rust closure environment won't
        // be deallocated while we're invoking it.
        state.cnt++;
        const a = state.a;
        state.a = 0;
        try {
            return f(a, state.b, ...args);
        } finally {
            if (--state.cnt === 0) {
                wasm.__wbindgen_export_2.get(state.dtor)(a, state.b);

            } else {
              state.a = a;
            }
        }
    };
  real.original = state;

  return real;
}

  function __wbg_adapter_42(arg0, arg1, arg2) {
    try {
      const retptr = wasm.__wbindgen_add_to_stack_pointer(-16);
      wasm._dyn_core__ops__function__FnMut__A____Output___R_as_wasm_bindgen__closure__WasmClosure___describe__invoke__hb4f2a2c4b323ad31(retptr, arg0, arg1, addHeapObject(arg2));
      var r0 = getInt32Memory0()[retptr / 4 + 0];
      var r1 = getInt32Memory0()[retptr / 4 + 1];
      if (r1) {
        throw takeObject(r0);
      }
    } finally {
      wasm.__wbindgen_add_to_stack_pointer(16);
    }
  }

  function __wbg_adapter_45(arg0, arg1) {
    wasm._dyn_core__ops__function__FnMut_____Output___R_as_wasm_bindgen__closure__WasmClosure___describe__invoke__ha68f87c05c57114b(arg0, arg1);
  }

  function __wbg_adapter_48(arg0, arg1, arg2) {
    wasm._dyn_core__ops__function__FnMut__A____Output___R_as_wasm_bindgen__closure__WasmClosure___describe__invoke__h4adc58b2e7650f37(arg0, arg1, addHeapObject(arg2));
  }

  function __wbg_adapter_51(arg0, arg1, arg2) {
    wasm._dyn_core__ops__function__FnMut__A____Output___R_as_wasm_bindgen__closure__WasmClosure___describe__invoke__hdacc065b17a63d45(arg0, arg1, addHeapObject(arg2));
  }

  function __wbg_adapter_54(arg0, arg1, arg2) {
    wasm._dyn_core__ops__function__FnMut__A____Output___R_as_wasm_bindgen__closure__WasmClosure___describe__invoke__hf16e572565278138(arg0, arg1, addHeapObject(arg2));
  }

  /**
   * @param {string} config_json
   * @returns {string}
   */
  function core_create_context(config_json) {
    try {
      const retptr = wasm.__wbindgen_add_to_stack_pointer(-16);
      const ptr0 = passStringToWasm0(config_json, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
      const len0 = WASM_VECTOR_LEN;
      wasm.core_create_context(retptr, ptr0, len0);
      var r0 = getInt32Memory0()[retptr / 4 + 0];
      var r1 = getInt32Memory0()[retptr / 4 + 1];
      return getStringFromWasm0(r0, r1);
    } finally {
      wasm.__wbindgen_add_to_stack_pointer(16);
      wasm.__wbindgen_free(r0, r1);
    }
}

/**
* @param {number} context
*/
function core_destroy_context(context) {
    wasm.core_destroy_context(context);
}

/**
* @param {number} context
* @param {string} function_name
* @param {any} params
* @param {number} request_id
*/
function core_request(context, function_name, params, request_id) {
    try {
        const retptr = wasm.__wbindgen_add_to_stack_pointer(-16);
        const ptr0 = passStringToWasm0(function_name, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
        const len0 = WASM_VECTOR_LEN;
        wasm.core_request(retptr, context, ptr0, len0, addHeapObject(params), request_id);
        var r0 = getInt32Memory0()[retptr / 4 + 0];
        var r1 = getInt32Memory0()[retptr / 4 + 1];
        if (r1) {
            throw takeObject(r0);
        }
    } finally {
        wasm.__wbindgen_add_to_stack_pointer(16);
    }
}

function handleError(f, args) {
    try {
        return f.apply(this, args);
    } catch (e) {
        wasm.__wbindgen_exn_store(addHeapObject(e));
    }
}

function getArrayU8FromWasm0(ptr, len) {
    return getUint8Memory0().subarray(ptr / 1, ptr / 1 + len);
}

async function load(module, imports) {
    if (typeof Response === 'function' && module instanceof Response) {
        if (typeof WebAssembly.instantiateStreaming === 'function') {
            try {
                return await WebAssembly.instantiateStreaming(module, imports);

            } catch (e) {
                if (module.headers.get('Content-Type') != 'application/wasm') {
                    console.warn("`WebAssembly.instantiateStreaming` failed because your server does not serve wasm with `application/wasm` MIME type. Falling back to `WebAssembly.instantiate` which is slower. Original error:\n", e);

                } else {
                    throw e;
                }
            }
        }

        const bytes = await module.arrayBuffer();
        return await WebAssembly.instantiate(bytes, imports);

    } else {
        const instance = await WebAssembly.instantiate(module, imports);

        if (instance instanceof WebAssembly.Instance) {
            return { instance, module };

        } else {
            return instance;
        }
    }
}

function getImports() {
  const imports = {};
  imports.wbg = {};
  imports.wbg.__wbg_new_8d2af00bc1e329ee = function (arg0, arg1) {
    const ret = new Error(getStringFromWasm0(arg0, arg1));
    return addHeapObject(ret);
  };
  imports.wbg.__wbindgen_string_new = function (arg0, arg1) {
    const ret = getStringFromWasm0(arg0, arg1);
    return addHeapObject(ret);
  };
  imports.wbg.__wbindgen_memory = function () {
    const ret = wasm.memory;
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_buffer_3f3d764d4747d564 = function (arg0) {
    const ret = getObject(arg0).buffer;
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_newwithbyteoffsetandlength_d9aa266703cb98be = function (arg0, arg1, arg2) {
    const ret = new Uint8Array(getObject(arg0), arg1 >>> 0, arg2 >>> 0);
    return addHeapObject(ret);
  };
  imports.wbg.__wbindgen_object_drop_ref = function (arg0) {
    takeObject(arg0);
  };
  imports.wbg.__wbg_newwithlength_7c42f7e738a9d5d3 = function (arg0) {
    const ret = new Array(arg0 >>> 0);
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_set_a68214f35c417fa9 = function (arg0, arg1, arg2) {
    getObject(arg0)[arg1 >>> 0] = takeObject(arg2);
  };
  imports.wbg.__wbg_newwithu8arraysequence_f863246af83e1785 = function () {
    return handleError(function (arg0) {
      const ret = new Blob(getObject(arg0));
      return addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_get_765201544a2b6869 = function () {
    return handleError(function (arg0, arg1) {
      const ret = Reflect.get(getObject(arg0), getObject(arg1));
      return addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_instanceof_ArrayBuffer_e5e48f4762c5610b = function (arg0) {
    let result;
    try {
      result = getObject(arg0) instanceof ArrayBuffer;
    } catch {
      result = false;
    }
    const ret = result;
    return ret;
  };
  imports.wbg.__wbindgen_is_object = function (arg0) {
    const val = getObject(arg0);
    const ret = typeof (val) === 'object' && val !== null;
    return ret;
  };
  imports.wbg.__wbg_new_8c3f0052272a457a = function (arg0) {
    const ret = new Uint8Array(getObject(arg0));
    return addHeapObject(ret);
  };
  imports.wbg.__wbindgen_object_clone_ref = function (arg0) {
    const ret = getObject(arg0);
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_keys_0702294afaeb6044 = function (arg0) {
    const ret = Object.keys(getObject(arg0));
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_length_6e3bbe7c8bd4dbd8 = function (arg0) {
    const ret = getObject(arg0).length;
    return ret;
  };
  imports.wbg.__wbg_get_57245cc7d7c7619d = function (arg0, arg1) {
    const ret = getObject(arg0)[arg1 >>> 0];
    return addHeapObject(ret);
  };
  imports.wbg.__wbindgen_string_get = function (arg0, arg1) {
    const obj = getObject(arg1);
    const ret = typeof (obj) === 'string' ? obj : undefined;
    var ptr0 = isLikeNone(ret) ? 0 : passStringToWasm0(ret, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
    var len0 = WASM_VECTOR_LEN;
    getInt32Memory0()[arg0 / 4 + 1] = len0;
    getInt32Memory0()[arg0 / 4 + 0] = ptr0;
  };
  imports.wbg.__wbg_new0_a57059d72c5b7aee = function () {
    const ret = new Date();
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_getTime_cb82adb2556ed13e = function (arg0) {
    const ret = getObject(arg0).getTime();
    return ret;
  };
  imports.wbg.__wbindgen_boolean_get = function (arg0) {
    const v = getObject(arg0);
    const ret = typeof (v) === 'boolean' ? (v ? 1 : 0) : 2;
    return ret;
  };
  imports.wbg.__wbindgen_is_bigint = function (arg0) {
    const ret = typeof (getObject(arg0)) === 'bigint';
    return ret;
  };
  imports.wbg.__wbg_BigInt_06819bca5a5bedef = function (arg0) {
    const ret = BigInt(getObject(arg0));
    return ret;
  };
  imports.wbg.__wbg_BigInt_d0c7d465bfa30d3b = function (arg0) {
    const ret = BigInt(arg0);
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_is_40a66842732708e7 = function (arg0, arg1) {
    const ret = Object.is(getObject(arg0), getObject(arg1));
    return ret;
  };
  imports.wbg.__wbindgen_number_get = function (arg0, arg1) {
    const obj = getObject(arg1);
    const ret = typeof (obj) === 'number' ? obj : undefined;
    getFloat64Memory0()[arg0 / 8 + 1] = isLikeNone(ret) ? 0 : ret;
    getInt32Memory0()[arg0 / 4 + 0] = !isLikeNone(ret);
  };
  imports.wbg.__wbg_isSafeInteger_dfa0593e8d7ac35a = function (arg0) {
    const ret = Number.isSafeInteger(getObject(arg0));
    return ret;
  };
  imports.wbg.__wbg_isArray_27c46c67f498e15d = function (arg0) {
    const ret = Array.isArray(getObject(arg0));
    return ret;
  };
  imports.wbg.__wbg_iterator_6f9d4f28845f426c = function () {
    const ret = Symbol.iterator;
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_BigInt_67359e71cae1c6c9 = function (arg0) {
    const ret = BigInt(getObject(arg0));
    return ret;
  };
  imports.wbg.__wbg_BigInt_1fab4952b6c4a499 = function (arg0) {
    const ret = BigInt(BigInt.asUintN(64, arg0));
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_entries_65a76a413fc91037 = function (arg0) {
    const ret = Object.entries(getObject(arg0));
    return addHeapObject(ret);
  };
  imports.wbg.__wbindgen_is_undefined = function (arg0) {
    const ret = getObject(arg0) === undefined;
    return ret;
  };
  imports.wbg.__wbindgen_is_null = function (arg0) {
    const ret = getObject(arg0) === null;
    return ret;
  };
  imports.wbg.__wbg_String_c9c0f9be374874ba = function (arg0, arg1) {
    const ret = String(getObject(arg1));
    const ptr0 = passStringToWasm0(ret, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
    const len0 = WASM_VECTOR_LEN;
    getInt32Memory0()[arg0 / 4 + 1] = len0;
    getInt32Memory0()[arg0 / 4 + 0] = ptr0;
  };
  imports.wbg.__wbindgen_number_new = function (arg0) {
    const ret = arg0;
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_new_0b9bfdd97583284e = function () {
    const ret = new Object();
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_String_33099856e8a8246a = function (arg0, arg1) {
    const ret = String(getObject(arg1));
    const ptr0 = passStringToWasm0(ret, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
    const len0 = WASM_VECTOR_LEN;
    getInt32Memory0()[arg0 / 4 + 1] = len0;
    getInt32Memory0()[arg0 / 4 + 0] = ptr0;
  };
  imports.wbg.__wbg_coreresponsehandler_a52879454c60c764 = function (arg0, arg1, arg2, arg3) {
    core_response_handler(arg0 >>> 0, takeObject(arg1), arg2 >>> 0, arg3 !== 0);
  };
  imports.wbg.__wbg_message_fe2af63ccc8985bc = function (arg0) {
    const ret = getObject(arg0).message;
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_new_1d9a920c6bfc44a8 = function () {
    const ret = new Array();
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_new_268f7b7dd3430798 = function () {
    const ret = new Map();
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_set_933729cf5b66ac11 = function (arg0, arg1, arg2) {
    const ret = getObject(arg0).set(getObject(arg1), getObject(arg2));
    return addHeapObject(ret);
  };
  imports.wbg.__wbindgen_is_string = function (arg0) {
    const ret = typeof (getObject(arg0)) === 'string';
    return ret;
  };
  imports.wbg.__wbg_set_fbc33d020f507b72 = function (arg0, arg1, arg2) {
    getObject(arg0)[takeObject(arg1)] = takeObject(arg2);
  };
  imports.wbg.__wbg_process_0cc2ada8524d6f83 = function (arg0) {
    const ret = getObject(arg0).process;
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_versions_c11acceab27a6c87 = function (arg0) {
    const ret = getObject(arg0).versions;
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_node_7ff1ce49caf23815 = function (arg0) {
    const ret = getObject(arg0).node;
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_static_accessor_NODE_MODULE_cf6401cc1091279e = function () {
    const ret = module;
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_require_a746e79b322b9336 = function () {
    return handleError(function (arg0, arg1, arg2) {
      const ret = getObject(arg0).require(getStringFromWasm0(arg1, arg2));
      return addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_crypto_2036bed7c44c25e7 = function (arg0) {
    const ret = getObject(arg0).crypto;
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_msCrypto_a21fc88caf1ecdc8 = function (arg0) {
    const ret = getObject(arg0).msCrypto;
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_newwithlength_f5933855e4f48a19 = function (arg0) {
    const ret = new Uint8Array(arg0 >>> 0);
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_transaction_cce96cbebd81fe1c = function () {
    return handleError(function (arg0, arg1, arg2, arg3) {
      const ret = getObject(arg0).transaction(getStringFromWasm0(arg1, arg2), takeObject(arg3));
      return addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_setoncomplete_3e57a8cec8327f66 = function (arg0, arg1) {
    getObject(arg0).oncomplete = getObject(arg1);
  };
  imports.wbg.__wbg_setonerror_00051c0213f27b2c = function (arg0, arg1) {
    getObject(arg0).onerror = getObject(arg1);
  };
  imports.wbg.__wbg_setonabort_404bee3b9940d03d = function (arg0, arg1) {
    getObject(arg0).onabort = getObject(arg1);
  };
  imports.wbg.__wbg_item_52a6bec36314687b = function (arg0, arg1, arg2) {
    const ret = getObject(arg1).item(arg2 >>> 0);
    var ptr0 = isLikeNone(ret) ? 0 : passStringToWasm0(ret, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
    var len0 = WASM_VECTOR_LEN;
    getInt32Memory0()[arg0 / 4 + 1] = len0;
    getInt32Memory0()[arg0 / 4 + 0] = ptr0;
  };
  imports.wbg.__wbg_objectStore_f17976b0e6377830 = function () {
    return handleError(function (arg0, arg1, arg2) {
      const ret = getObject(arg0).objectStore(getStringFromWasm0(arg1, arg2));
      return addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_target_bf704b7db7ad1387 = function (arg0) {
    const ret = getObject(arg0).target;
    return isLikeNone(ret) ? 0 : addHeapObject(ret);
  };
  imports.wbg.__wbg_readyState_fb287f170113917c = function (arg0) {
    const ret = getObject(arg0).readyState;
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_setonsuccess_5f71593bc51653a3 = function (arg0, arg1) {
    getObject(arg0).onsuccess = getObject(arg1);
  };
  imports.wbg.__wbg_setonerror_d5771cc5bf9ea74c = function (arg0, arg1) {
    getObject(arg0).onerror = getObject(arg1);
  };
  imports.wbg.__wbindgen_cb_drop = function (arg0) {
    const obj = takeObject(arg0).original;
    if (obj.cnt-- == 1) {
      obj.a = 0;
      return true;
    }
    const ret = false;
    return ret;
  };
  imports.wbg.__wbg_next_aaef7c8aa5e212ac = function () {
    return handleError(function (arg0) {
      const ret = getObject(arg0).next();
      return addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_done_1b73b0672e15f234 = function (arg0) {
    const ret = getObject(arg0).done;
    return ret;
  };
  imports.wbg.__wbg_value_1ccc36bc03462d71 = function (arg0) {
    const ret = getObject(arg0).value;
    return addHeapObject(ret);
  };
  imports.wbg.__wbindgen_is_function = function (arg0) {
    const ret = typeof (getObject(arg0)) === 'function';
    return ret;
  };
  imports.wbg.__wbg_call_97ae9d8645dc388b = function () {
    return handleError(function (arg0, arg1) {
      const ret = getObject(arg0).call(getObject(arg1));
      return addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_next_579e583d33566a86 = function (arg0) {
    const ret = getObject(arg0).next;
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_self_6d479506f72c6a71 = function () {
    return handleError(function () {
      const ret = self.self;
      return addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_window_f2557cc78490aceb = function () {
    return handleError(function () {
      const ret = window.window;
      return addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_globalThis_7f206bda628d5286 = function () {
    return handleError(function () {
      const ret = globalThis.globalThis;
      return addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_global_ba75c50d1cf384f4 = function () {
    return handleError(function () {
      const ret = global.global;
      return addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_newnoargs_b5b063fc6c2f0376 = function (arg0, arg1) {
    const ret = new Function(getStringFromWasm0(arg0, arg1));
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_set_83db9690f9353e79 = function (arg0, arg1, arg2) {
    getObject(arg0).set(getObject(arg1), arg2 >>> 0);
  };
  imports.wbg.__wbg_length_9e1ae1900cb0fbd5 = function (arg0) {
    const ret = getObject(arg0).length;
    return ret;
  };
  imports.wbg.__wbg_has_8359f114ce042f5a = function () {
    return handleError(function (arg0, arg1) {
      const ret = Reflect.has(getObject(arg0), getObject(arg1));
      return ret;
    }, arguments)
  };
  imports.wbg.__wbg_set_bf3f89b92d5a34bf = function () {
    return handleError(function (arg0, arg1, arg2) {
      const ret = Reflect.set(getObject(arg0), getObject(arg1), getObject(arg2));
      return ret;
    }, arguments)
  };
  imports.wbg.__wbg_randomFillSync_065afffde01daa66 = function () {
    return handleError(function (arg0, arg1, arg2) {
      getObject(arg0).randomFillSync(getArrayU8FromWasm0(arg1, arg2));
    }, arguments)
  };
  imports.wbg.__wbg_subarray_58ad4efbb5bcb886 = function (arg0, arg1, arg2) {
    const ret = getObject(arg0).subarray(arg1 >>> 0, arg2 >>> 0);
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_getRandomValues_b99eec4244a475bb = function () {
    return handleError(function (arg0, arg1) {
      getObject(arg0).getRandomValues(getObject(arg1));
    }, arguments)
  };
  imports.wbg.__wbg_self_7eede1f4488bf346 = function () {
    return handleError(function () {
      const ret = self.self;
      return addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_crypto_c909fb428dcbddb6 = function (arg0) {
    const ret = getObject(arg0).crypto;
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_msCrypto_511eefefbfc70ae4 = function (arg0) {
    const ret = getObject(arg0).msCrypto;
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_static_accessor_MODULE_ef3aa2eb251158a5 = function () {
    const ret = module;
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_require_900d5c3984fe7703 = function (arg0, arg1, arg2) {
    const ret = getObject(arg0).require(getStringFromWasm0(arg1, arg2));
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_getRandomValues_307049345d0bd88c = function (arg0) {
    const ret = getObject(arg0).getRandomValues;
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_randomFillSync_85b3f4c52c56c313 = function (arg0, arg1, arg2) {
    getObject(arg0).randomFillSync(getArrayU8FromWasm0(arg1, arg2));
  };
  imports.wbg.__wbg_getRandomValues_cd175915511f705e = function (arg0, arg1) {
    getObject(arg0).getRandomValues(getObject(arg1));
  };
  imports.wbg.__wbg_instanceof_Uint8Array_971eeda69eb75003 = function (arg0) {
    let result;
    try {
      result = getObject(arg0) instanceof Uint8Array;
    } catch {
      result = false;
    }
    const ret = result;
    return ret;
  };
  imports.wbg.__wbg_put_84e7fc93eee27b28 = function () {
    return handleError(function (arg0, arg1, arg2) {
      const ret = getObject(arg0).put(getObject(arg1), getObject(arg2));
      return addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_delete_8abedd1043b4105d = function () {
    return handleError(function (arg0, arg1) {
      const ret = getObject(arg0).delete(getObject(arg1));
      return addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_setonversionchange_840d65cd0888dfb0 = function (arg0, arg1) {
    getObject(arg0).onversionchange = getObject(arg1);
  };
  imports.wbg.__wbg_setonupgradeneeded_17d0b9530f1e0cac = function (arg0, arg1) {
    getObject(arg0).onupgradeneeded = getObject(arg1);
  };
  imports.wbg.__wbg_setonblocked_e66d6be5c879980d = function (arg0, arg1) {
    getObject(arg0).onblocked = getObject(arg1);
  };
  imports.wbg.__wbg_message_a7af3ee0cc0fe28d = function (arg0, arg1) {
    const ret = getObject(arg1).message;
    const ptr0 = passStringToWasm0(ret, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
    const len0 = WASM_VECTOR_LEN;
    getInt32Memory0()[arg0 / 4 + 1] = len0;
    getInt32Memory0()[arg0 / 4 + 0] = ptr0;
  };
  imports.wbg.__wbg_Window_5684341ff6dfe3ad = function (arg0) {
    const ret = getObject(arg0).Window;
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_WorkerGlobalScope_e0447ffcae8bb272 = function (arg0) {
    const ret = getObject(arg0).WorkerGlobalScope;
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_indexedDB_050f0962ab607ac5 = function () {
    return handleError(function (arg0) {
      const ret = getObject(arg0).indexedDB;
      return isLikeNone(ret) ? 0 : addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_indexedDB_8d9e9ab4616df7f0 = function () {
    return handleError(function (arg0) {
      const ret = getObject(arg0).indexedDB;
      return isLikeNone(ret) ? 0 : addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_open_a31c3fe1fdc244eb = function () {
    return handleError(function (arg0, arg1, arg2) {
      const ret = getObject(arg0).open(getStringFromWasm0(arg1, arg2));
      return addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_setTimeout_d6fcf0d9067b8e64 = function () {
    return handleError(function (arg0, arg1, arg2) {
      const ret = getObject(arg0).setTimeout(getObject(arg1), arg2);
      return ret;
    }, arguments)
  };
  imports.wbg.__wbg_clearTimeout_7d6f7bfeed34b348 = function (arg0, arg1) {
    getObject(arg0).clearTimeout(arg1);
  };
  imports.wbg.__wbg_newwithstrandinit_05d7180788420c40 = function () {
    return handleError(function (arg0, arg1, arg2) {
      const ret = new Request(getStringFromWasm0(arg0, arg1), getObject(arg2));
      return addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_headers_85824e993aa739bf = function (arg0) {
    const ret = getObject(arg0).headers;
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_set_992c1d31586b2957 = function () {
    return handleError(function (arg0, arg1, arg2, arg3, arg4) {
      getObject(arg0).set(getStringFromWasm0(arg1, arg2), getStringFromWasm0(arg3, arg4));
    }, arguments)
  };
  imports.wbg.__wbg_fetch_0fe04905cccfc2aa = function (arg0, arg1) {
    const ret = getObject(arg0).fetch(getObject(arg1));
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_instanceof_Response_eaa426220848a39e = function (arg0) {
    let result;
    try {
      result = getObject(arg0) instanceof Response;
    } catch {
      result = false;
    }
    const ret = result;
    return ret;
  };
  imports.wbg.__wbg_status_c4ef3dd591e63435 = function (arg0) {
    const ret = getObject(arg0).status;
    return ret;
  };
  imports.wbg.__wbg_url_74285ddf2747cb3d = function (arg0, arg1) {
    const ret = getObject(arg1).url;
    const ptr0 = passStringToWasm0(ret, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
    const len0 = WASM_VECTOR_LEN;
    getInt32Memory0()[arg0 / 4 + 1] = len0;
    getInt32Memory0()[arg0 / 4 + 0] = ptr0;
  };
  imports.wbg.__wbg_newwithstr_d5b5f9b985ee84fb = function () {
    return handleError(function (arg0, arg1, arg2, arg3) {
      const ret = new WebSocket(getStringFromWasm0(arg0, arg1), getStringFromWasm0(arg2, arg3));
      return addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_new_d29e507f6606de91 = function () {
    return handleError(function (arg0, arg1) {
      const ret = new WebSocket(getStringFromWasm0(arg0, arg1));
      return addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_setonmessage_c5a806b62a0c5607 = function (arg0, arg1) {
    getObject(arg0).onmessage = getObject(arg1);
  };
  imports.wbg.__wbg_setonopen_9ce48dce57e549b5 = function (arg0, arg1) {
    getObject(arg0).onopen = getObject(arg1);
  };
  imports.wbg.__wbg_setonerror_02393260b3e29972 = function (arg0, arg1) {
    getObject(arg0).onerror = getObject(arg1);
  };
  imports.wbg.__wbg_send_80b256d87a6779e5 = function () {
    return handleError(function (arg0, arg1, arg2) {
      getObject(arg0).send(getStringFromWasm0(arg1, arg2));
    }, arguments)
  };
  imports.wbg.__wbg_data_7b1f01f4e6a64fbe = function (arg0) {
    const ret = getObject(arg0).data;
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_stringify_d6471d300ded9b68 = function () {
    return handleError(function (arg0) {
      const ret = JSON.stringify(getObject(arg0));
      return addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_objectStoreNames_8c06c40d2b05141c = function (arg0) {
    const ret = getObject(arg0).objectStoreNames;
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_createObjectStore_d3e2789c13dde1fc = function () {
    return handleError(function (arg0, arg1, arg2) {
      const ret = getObject(arg0).createObjectStore(getStringFromWasm0(arg1, arg2));
      return addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_instanceof_Error_56b496a10a56de66 = function (arg0) {
    let result;
    try {
      result = getObject(arg0) instanceof Error;
    } catch {
      result = false;
    }
    const ret = result;
    return ret;
  };
  imports.wbg.__wbg_getTimezoneOffset_89bd4275e1ca8341 = function (arg0) {
    const ret = getObject(arg0).getTimezoneOffset();
    return ret;
  };
  imports.wbg.__wbindgen_debug_string = function (arg0, arg1) {
    const ret = debugString(getObject(arg1));
    const ptr0 = passStringToWasm0(ret, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
    const len0 = WASM_VECTOR_LEN;
    getInt32Memory0()[arg0 / 4 + 1] = len0;
    getInt32Memory0()[arg0 / 4 + 0] = ptr0;
  };
  imports.wbg.__wbindgen_throw = function (arg0, arg1) {
    throw new Error(getStringFromWasm0(arg0, arg1));
  };
  imports.wbg.__wbg_then_cedad20fbbd9418a = function (arg0, arg1, arg2) {
    const ret = getObject(arg0).then(getObject(arg1), getObject(arg2));
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_resolve_99fe17964f31ffc0 = function (arg0) {
    const ret = Promise.resolve(getObject(arg0));
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_then_11f7a54d67b4bfad = function (arg0, arg1) {
    const ret = getObject(arg0).then(getObject(arg1));
    return addHeapObject(ret);
  };
  imports.wbg.__wbg_result_9e399c14676970d9 = function () {
    return handleError(function (arg0) {
      const ret = getObject(arg0).result;
      return addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_error_aacf5ac191e54ed0 = function () {
    return handleError(function (arg0) {
      const ret = getObject(arg0).error;
      return isLikeNone(ret) ? 0 : addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_get_6285bf458a1ee758 = function () {
    return handleError(function (arg0, arg1) {
      const ret = getObject(arg0).get(getObject(arg1));
      return addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbg_instanceof_Window_acc97ff9f5d2c7b4 = function (arg0) {
    let result;
    try {
      result = true;
    } catch {
      result = false;
    }
    const ret = result;
    return ret;
  };
  imports.wbg.__wbg_close_45d053bea59e7746 = function () {
    return handleError(function (arg0) {
      getObject(arg0).close();
    }, arguments)
  };
  imports.wbg.__wbg_text_1169d752cc697903 = function () {
    return handleError(function (arg0) {
      const ret = getObject(arg0).text();
      return addHeapObject(ret);
    }, arguments)
  };
  imports.wbg.__wbindgen_closure_wrapper943 = function (arg0, arg1, arg2) {
    const ret = makeClosure(arg0, arg1, 46, __wbg_adapter_36);
    return addHeapObject(ret);
  };
  imports.wbg.__wbindgen_closure_wrapper955 = function (arg0, arg1, arg2) {
    const ret = makeClosure(arg0, arg1, 43, __wbg_adapter_39);
    return addHeapObject(ret);
  };
  imports.wbg.__wbindgen_closure_wrapper6218 = function (arg0, arg1, arg2) {
    const ret = makeMutClosure(arg0, arg1, 243, __wbg_adapter_42);
    return addHeapObject(ret);
  };
  imports.wbg.__wbindgen_closure_wrapper6286 = function (arg0, arg1, arg2) {
    const ret = makeMutClosure(arg0, arg1, 237, __wbg_adapter_45);
    return addHeapObject(ret);
  };
  imports.wbg.__wbindgen_closure_wrapper6765 = function (arg0, arg1, arg2) {
    const ret = makeMutClosure(arg0, arg1, 246, __wbg_adapter_48);
    return addHeapObject(ret);
  };
  imports.wbg.__wbindgen_closure_wrapper6766 = function (arg0, arg1, arg2) {
    const ret = makeMutClosure(arg0, arg1, 1206, __wbg_adapter_51);
    return addHeapObject(ret);
  };
  imports.wbg.__wbindgen_closure_wrapper6767 = function (arg0, arg1, arg2) {
    const ret = makeMutClosure(arg0, arg1, 240, __wbg_adapter_54);
    return addHeapObject(ret);
  };

  return imports;
}

function initMemory(imports, maybe_memory) {

}

function finalizeInit(instance, module) {
    wasm = instance.exports;
    init.__wbindgen_wasm_module = module;
    cachedFloat64Memory0 = new Float64Array();
    cachedInt32Memory0 = new Int32Array();
    cachedUint8Memory0 = new Uint8Array();


    return wasm;
}

  function initSync(module) {
    const imports = getImports();

    initMemory(imports);

    if (!(module instanceof WebAssembly.Module)) {
      module = new WebAssembly.Module(module);
    }

    const instance = new WebAssembly.Instance(module, imports);

    return finalizeInit(instance, module);
  }

async function init(input) {
    if (typeof input === 'undefined') {    }
    const imports = getImports();

    if (typeof input === 'string' || (typeof Request === 'function' && input instanceof Request) || (typeof URL === 'function' && input instanceof URL)) {
        input = fetch(input);
    }

    initMemory(imports);

    const { instance, module } = await load(await input, imports);

    return finalizeInit(instance, module);
}


//****************************************************************** WRAPPER END
    function replaceUndefinedWithNulls(value) {
        if (value === undefined) {
            return null;
        }
        if (value instanceof Blob) {
            return value;
        }
        if (typeof value === "object" && value !== null) {
            const result = Array.isArray(value) ? [] : {};
            for (const key in value) {
                result[key] = replaceUndefinedWithNulls(value[key]);
            }
            return result;
        }
        return value;
    }

    async function replaceBlobsWithArrayBuffers(value) {
        if (value instanceof Blob) {
            return await value.arrayBuffer();
        }
        if (typeof value === "bigint") {
            if (value < Number.MAX_SAFE_INTEGER && value > Number.MIN_SAFE_INTEGER) {
                return Number(value);
            } else {
                return value.toString();
            }
        }
        if (typeof value === "object" && value !== null) {
            const result = Array.isArray(value) ? [] : {};
            for (const key in value) {
                result[key] = await replaceBlobsWithArrayBuffers(value[key]);
            }
            return result;
        }
        return value;
    }


    let deferredCreateContext = [];
    let responseHandler = null;

    function core_response_handler(request_id, params, response_type, finished) {
        if (responseHandler) {
            responseHandler(
                request_id,
                params,
                response_type,
                finished,
            );
        }
    }

    (async () => {
        await init(await loadModule());
        for (const createContext of deferredCreateContext) {
            createContext.resolve(core_create_context(createContext.configJson));
        }
        deferredCreateContext = null;
    })();

    return Promise.resolve({
        setResponseParamsHandler: (handler) => {
            responseHandler = handler;
        },
        createContext: (configJson) => {
            return deferredCreateContext === null
                ? Promise.resolve(core_create_context(configJson))
                : new Promise((resolve) => {
                    deferredCreateContext.push({
                        configJson,
                        resolve,
                    });
                });
        },
        destroyContext: (context) => {
            core_destroy_context(context);
        },
        sendRequestParams: (context, requestId, functionName, functionParams) => {
            (async () => {
                core_request(
                    context,
                    functionName,
                    await replaceBlobsWithArrayBuffers(functionParams),
                    requestId,
                );
            })();
        },
    });

}

export function libWeb() {
    return options && options.disableSeparateWorker ? withoutSeparateWorker() : withSeparateWorker();
}
