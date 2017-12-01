namespace CRDTs

type GCounter<'K> when 'K : comparison = { payload: Map<'K, uint32> }

[<AutoOpen>]
module GCounter =
  let init<'K when 'K : comparison> =
    { payload = Map([]) }
  ///**Description**
  /// Increments the counter replicate by one.
  ///**Parameters**
  ///  * `key` - parameter of type `'K` The replica key.
  ///  * `counter` - parameter of type `GCounter<'K>` The counter replica to increment.
  ///
  ///**Output Type**
  ///  * `GCounter<'K>`
  let increment<'K when 'K : comparison> (key: 'K) (counter: GCounter<'K>) =
    match Map.tryFind key counter.payload with
    | Some x -> { payload = Map.add key (x + uint32 1) counter.payload }
    | None -> { payload = Map([(key, uint32 1)]) }


  ///**Description**
  /// Increment the counter replica by N ticks.
  ///**Parameters**
  ///  * `key` - parameter of type `'K` The replica key.
  ///  * `counter` - parameter of type `GCounter<'K>` The counter replica to increment.
  ///  * `ticks` - parameter of type `uint32` Number to increment the counter by.
  ///
  ///**Output Type**
  ///  * `GCounter<'K>`
  ///
  ///**Exceptions**
  ///
  let incrementBy<'K when 'K : comparison> (key:'K) (counter: GCounter<'K>) (ticks:uint32) =
    match Map.tryFind key counter.payload with
    | Some x -> { payload = Map.add key (x + ticks) counter.payload }
    | None -> { payload = Map([(key, ticks)])}

  ///**Description**
  /// Queries a GCounter replica for its current value.
  ///**Parameters**
  ///  * `counter` - parameter of type `GCounter<'K>` The counter replicate get the value of.
  ///
  ///**Output Type**
  ///  * `uint32` The current value of this counter replica.
  let query<'K when 'K : comparison> (counter:GCounter<'K>) =
    let folder s _ v =
        s + v
    let initialState = uint32 0
    Map.fold folder initialState counter.payload


  ///**Description**
  /// Compares two GCounter replicas for equality.
  ///**Parameters**
  ///  * `x` - parameter of type `GCounter<'K>` The first counter to compare.
  ///  * `y` - parameter of type `GCounter<'K>` The second counter to compare.
  ///
  ///**Output Type**
  ///  * `bool` true if the counters are equal, else false.
  let compare<'K when 'K : comparison> (x:GCounter<'K>) (y:GCounter<'K>) =
    let pred k xv =
        match Map.tryFind k y.payload with
        | Some yv -> xv <= yv
        | None -> true
    Map.forall pred x.payload


  ///**Description**
  /// Merges two GCounter replicas into a new one.
  ///**Parameters**
  ///  * `x` - parameter of type `GCounter<'K>` First counter to merge.
  ///  * `y` - parameter of type `GCounter<'K>` Second counter to merge.
  ///
  ///**Output Type**
  ///  * `GCounter<'K>` A new counter containing the merge result.
  let merge<'K when 'K : comparison> (x:GCounter<'K>) (y:GCounter<'K>) =
    let merger k xv =
        match Map.tryFind k y.payload with
        | Some yv -> if xv <= yv then yv else xv
        | None -> xv
    let mergedPayload = Map.map merger x.payload
    { payload = mergedPayload }
